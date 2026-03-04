using Dapper;
using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.DTOs.Transaction;
using FinanceCore.Application.Models;
using FinanceCore.Domain.Transactions;
using FinanceCore.Infrastructure.context;
using FinanceCore.Infrastructure.Mappers;

namespace FinanceCore.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public TransactionRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        private async Task<TransactionModel?> GetModelByIdAndUserIdAsync(Guid userId, Guid id, CancellationToken token = default)
        {
            using var connection = _connectionFactory.GetConnection();
            var sql = @"SELECT
                t.Id,
                t.AccountId,
                t.ToAccountId,
                t.CategoryId,
                t.Amount,
                t.TransactionTypeId,
                t.Date,
                t.CreatedAt,
                t.UpdatedAt,
                t.Description
            FROM Transactions t 
            INNER JOIN Accounts a 
            ON a.Id = t.AccountId
            WHERE t.Id = @Id AND a.UserId = @UserId";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id);
            parameters.Add("UserId", userId);
            var model = await connection.QuerySingleOrDefaultAsync<TransactionModel>(sql, parameters);
            return model;

        }
        public async Task<bool> IsExists(Guid userId, Guid id, CancellationToken token)
        {
            using var connection = _connectionFactory.GetConnection();
            var sql = @"SELECT 1 FROM transactions WHERE UserId = @Id AND Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);
            parameters.Add("UserId", userId);
            var result = await connection.ExecuteScalarAsync<int?>(sql, parameters);
            return result.HasValue;
        }
        public async Task<Transaction?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            var model = await _connectionFactory.ReadSingleAsync<TransactionModel, Guid>(
                "sp_GetTransactionById",
                id);
            if (model == null)
            {
                return null;
            }
            return TransactionMapper.MapToDomain(model);
        }

        public async Task<TransactionDto?> GetDtoByIdAndUserId(Guid userId, Guid id, CancellationToken token = default)
        {
            var model = await GetModelByIdAndUserIdAsync(userId, id, token);
            if (model is null) return null;

            return new TransactionDto(
                model.Id,
                model.AccountId,
                model.ToAccountId,
                model.CategoryId,
                model.Amount,
                model.Type,
                model.CreatedAt,
                model.Description);
        }

        public async Task<Transaction?> GetByIdAndUserId(Guid userId, Guid id, CancellationToken token = default)
        {
            var model = await GetModelByIdAndUserIdAsync(userId, id, token);
            if (model == null) return null;

            return TransactionMapper.MapToDomain(model);
        }

        public async Task<IEnumerable<Transaction>> GetByAccountIdAsync(Guid accountId, CancellationToken token = default)
        {
            var models = await _connectionFactory.ReadListAsync<TransactionModel>(
                "sp_GetTransactionsByAccountId",
                new { AccountId = accountId });
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

        public async Task<CreateTransferDto> TransferAsync(Transaction transaction, CancellationToken token = default)
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

            return new CreateTransferDto(result.CreditTransactionId, result.DebitTransactionId, model.AccountId, model.ToAccountId, model.Amount, result.SourceBalance, result.DestinationBalance, result.TransferDate);
        }

        public async Task<CreateTransactionDto> IncomeAsync(Transaction transaction, CancellationToken token = default)
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
            return new CreateTransactionDto(result.Id, model.AccountId, result.CategoryId, result.Amount, model.Type, model.Date, result.Description);
        }

        public async Task<CreateTransactionDto> ExpenseAsync(Transaction transaction, CancellationToken token = default)
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
            return new CreateTransactionDto(result.Id, model.AccountId, result.CategoryId, result.Amount, model.Type, model.Date, result.Description);
        }

        public async Task UpdateAsync(Transaction transaction, CancellationToken token = default)
        {
            var model = TransactionMapper.MapToModel(transaction);
            await _connectionFactory.ExecuteNonQueryAsync(
                "sp_UpdateTransaction",
                model
               );
        }

        public async Task DeleteAsync(Guid id, CancellationToken token = default)
        {
            await _connectionFactory.ExecuteNonQueryAsync(
                "sp_DeleteTransaction",
                new { id });
        }
        public async Task<decimal> GetTotalSpentAsync(Guid categoryId, DateTime start, DateTime end, byte type = 2)
        {
            // Fetch all filtered transactions (paginated internally)
            var transactions = await FetchAllTransactionsAsync(categoryId, start, end, type);

            // Sum amounts
            return transactions.Sum(t => t.Amount);
        }
        public async Task<ReportModel?> GetMonthlySummary(Guid AccountId, DateTime Start , DateTime End)
        {
            using var connection = _connectionFactory.GetConnection();
            var sql = @"
                SELECT
                    SUM(CASE WHEN TransactionType = 'Income' THEN Amount ELSE 0 END) AS TotalIncome,
                    SUM(CASE WHEN TransactionType = 'Expense' THEN Amount ELSE 0 END) AS TotalExpenses,
                FROM Transactions
                WHERE AccountId = @AccountId
                AND TransactionDate >= @StartDate
                AND TransactionDate < @EndDate;";
             return await connection.QueryFirstOrDefaultAsync<ReportModel>(sql, 
                new { AccountId = AccountId , StartDate = Start , EndDate = End  });


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
        public async Task<IEnumerable<SpendingByCategoryDto>> GetSpendingByCategory(
            Guid userId, Guid? accountId, DateTime start, DateTime end)
        {
            using var connection = _connectionFactory.GetConnection();
            var sql = @"
            SELECT c.Name AS Category, SUM(t.Amount) AS Amount
            FROM Transactions t
            INNER JOIN Categories c ON c.Id = t.CategoryId
            INNER JOIN Accounts a ON a.Id = t.AccountId
                 WHERE t.TransactionTypeId = 2
                 AND a.UserId = @UserId
                 AND (@AccountId IS NULL OR t.AccountId = @AccountId)
                 AND t.CreatedAt >= @StartDate
                 AND t.CreatedAt < @EndDate
             GROUP BY c.Name
             ORDER BY Amount DESC";

            return await connection.QueryAsync<SpendingByCategoryDto>(sql, new
            {
                UserId = userId,
                AccountId = accountId,
                StartDate = start,
                EndDate = end
            });
        } 
    }
}