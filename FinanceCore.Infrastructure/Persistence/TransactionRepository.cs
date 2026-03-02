using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs.Transaction;
using Dapper;
using FinanceCore.Domain.Transactions;
using FinanceCore.Infrastructure.context;
using FinanceCore.Infrastructure.Mappers;
using FinanceCore.Infrastructure.Models;
using FinanceCore.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace FinanceCore.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public TransactionRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Transaction?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            var model =  await _connectionFactory.ReadSingleAsync<TransactionModel, Guid>(
                "sp_GetTransactionById",
                id);
            if (model == null) { 
                return null;
            }
            return TransactionMapper.MapToDomain(model);
        }
        public async Task<TransactionDto?> GetDtoByIdAndUserId(Guid UserId , Guid Id , CancellationToken token)
        {
            using var connection = _connectionFactory.GetConnection();
            var parameters = new DynamicParameters();
            var sql = "SELECT * FROM transactions WHERE";
            sql += " UserId = @UserId";
            parameters.Add("UserId",UserId);
            sql += " AND Id = @Id";
            parameters.Add("Id",Id);

            var model = await connection.QuerySingleOrDefaultAsync(sql, parameters);
            if (model == null) {
                return null;
            }
            return new TransactionDto(model.Id, model.AccountId, model.ToAccountId, model.CategoryId, model.Amount, model.Type, model.CreatedAt, model.Description);
        }
        public async Task<Transaction?> GetByIdAndUserId(Guid UserId, Guid Id, CancellationToken token = default)
        {
            using var connection = _connectionFactory.GetConnection();
            var parameters = new DynamicParameters();
            var sql = "SELECT * FROM transactions WHERE";
            sql += " UserId = @UserId";
            parameters.Add("UserId", UserId);
            sql += " AND Id = @Id";
            parameters.Add("Id", Id);

            var model = await connection.QuerySingleOrDefaultAsync(sql, parameters);
            if (model == null)
            {
                return null;
            }
            return TransactionMapper.MapToDomain(model);        }

        public async Task<IEnumerable<Transaction>> GetByAccountIdAsync(Guid accountId, CancellationToken token = default)
        {
            var models =  await _connectionFactory.ReadListAsync<TransactionModel>(
                "sp_GetTransactionsByAccountId",
                new {AccountId = accountId});
            return models.Select(model => TransactionMapper.MapToDomain(model));
        }

        public async Task AddAsync(Transaction transaction, CancellationToken token = default)
        {
            var model = TransactionMapper.MapToModel(transaction);
             await _connectionFactory.ExecuteNonQueryAsync(
                "sp_CreateTransaction",
                model
                );
        }
        public async Task<CreateTransferDto> TransferAsync(Transaction transaction, CancellationToken token)
        {
            var model = TransactionMapper.MapToModel(transaction);

            // Use QuerySingleAsync to get the SP result
            var result = await _connectionFactory.QuerySingleAsync<TransferModel>(
                "sp_Transfer",
                new
                {
                    SourceAccountId = model.AccountId,
                    DestinationAccountId = model.ToAccountId,
                    Amount = model.Amount,
                    Description = model.Description
                }
                );

            return new CreateTransferDto(result.CreditTransactionId,result.DebitTransactionId,model.AccountId,model.ToAccountId,model.Amount,result.SourceBalance,result.DestinationBalance,result.TransferDate);
        }
        public async Task<CreateTransactionDto> IncomeAsync(Transaction transaction, CancellationToken token)
        {
            var model = TransactionMapper.MapToModel(transaction);
            var result = await _connectionFactory.QuerySingleAsync<TransactionModel>(
                "sp_CreateIncome",
                new
                {
                    AccountId = model.AccountId,
                    CategoryId = model.CategoryId,
                    Amount = model.Amount,
                    Description = model?.Description,
                }
                );
            return new CreateTransactionDto(result.Id ,model.AccountId,result.CategoryId , result.Amount ,model.Type,model.Date,result.Description);


        }
        public async Task<CreateTransactionDto> ExpenseAsync(Transaction transaction, CancellationToken token)
        {
            var model = TransactionMapper.MapToModel(transaction);
            var result = await _connectionFactory.QuerySingleAsync<TransactionModel>(
                "sp_CreateExpense",
                new
                {
                    AccountId = model.AccountId,
                    CategoryId = model.CategoryId,
                    Amount = model.Amount,
                    Description = model?.Description,
                }
                );
            return new CreateTransactionDto(result.Id ,model.AccountId,result.CategoryId , result.Amount ,model.Type,model.Date,result.Description);


        }

        public async Task UpdateAsync(Transaction transaction, CancellationToken token = default)
        {
           var model = TransactionMapper.MapToModel(transaction);
            await _connectionFactory.ExecuteNonQueryAsync(
                "sp_UpdateTransaction",
                model
               );
        }

        public async Task DeleteAsync(Transaction transaction, CancellationToken token = default)
        {
            await _connectionFactory.ExecuteNonQueryAsync(
                "sp_DeleteTransaction",
                new { transaction.Id });
        }
        public async Task<decimal> GetTotalSpentAsync(Guid categoryId, DateTime start, DateTime end, byte type = 2)
        {
            // Fetch all filtered transactions (paginated internally)
            var transactions = await FetchAllTransactionsAsync(categoryId, start, end, type);

            // Sum amounts
            return transactions.Sum(t => t.Amount);
        }
        public async Task<IEnumerable<TransactionDto>?> GetFiltredTransactionsAsync(
            Guid? categoryId,
            DateTime? start,
            DateTime? end,
            byte? type,
            int page,
            int pageSize)
        {
            return await FetchTransactionsPageAsync(null,categoryId, start, end, type, page, pageSize);
        }
        public async Task<IEnumerable<TransactionDto>?> FetchTransactionsByIdPageAsync(Guid AccountId,int Page , int PageSize)
        {
            return await FetchTransactionsPageAsync(AccountId, null, null, null, null, Page, PageSize);
        } 
        private async Task<IEnumerable<TransactionDto>?> FetchTransactionsPageAsync(Guid? AccountId ,Guid? CategoryId , DateTime? Start , DateTime? End ,byte? Type , int Page , int PageSize)
        {
            using var connection = _connectionFactory.GetConnection();
            var sql = @"SELECT * FROM Transactions WHERE 1 = 1";

            var parameters = new DynamicParameters();
            if (AccountId.HasValue)
            {
                sql += " AND AccountId = @AccountId";
                parameters.Add("AccountId", AccountId);
            }

            if (CategoryId.HasValue)
            {
                sql += " AND CategoryId = @CategoryId";
                parameters.Add("CategoryId", CategoryId);
            }
            if (Start.HasValue) {
                sql += " AND CreatedAt >= @Start";
                parameters.Add("Start", Start);


            }
            if (End.HasValue) {
                sql += " AND CreatedAt <= @End";
                parameters.Add("End", End);
            
            }
            if (Type.HasValue)
            {
                sql += " AND TransactionTypeId = @Type";
                parameters.Add("Type", Type);
            }

            // Order By CreatedAt
            sql += " ORDER BY CreatedAt";
            sql += " OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";


            parameters.Add("Offset", (Page - 1) * PageSize);
            parameters.Add("PageSize", PageSize);

            var model = await connection.QueryAsync<TransactionModel>(sql, parameters);
            return model.Select(model => new TransactionDto(model.Id, model.AccountId, model.ToAccountId, model.CategoryId, model.Amount, model.Type, model.CreatedAt, model.Description));
        }
        private async Task<IEnumerable<TransactionDto>> FetchAllTransactionsAsync(
            Guid? categoryId = null,
            DateTime? start = null,
            DateTime? end = null,
            byte? type = null)
        {
            var allTransactions = new List<TransactionDto>();
            int page = 1;
            const int pageSize = 100; // fetch in batches to avoid loading everything at once

            while (true)
            {
                var pageTransactions = (await FetchTransactionsPageAsync(null,categoryId, start, end, type, page, pageSize)).ToList();
                if (!pageTransactions.Any())
                    break;

                allTransactions.AddRange(pageTransactions);
                page++;
            }

            return allTransactions;
        }
    }
}