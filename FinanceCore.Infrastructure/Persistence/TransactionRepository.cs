using Dapper;
using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.DTOs.Transaction;
using FinanceCore.Application.Models;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Transactions;
using FinanceCore.Infrastructure.context;
using FinanceCore.Infrastructure.Mappers;
using System.Text;

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

            var sql = @"
                SELECT t.Id, t.AccountId, t.ToAccountId, t.CategoryId,
                       t.Amount, t.TransactionTypeId, t.Date, t.CreatedAt, t.UpdatedAt, t.Description
                FROM Transactions t
                INNER JOIN Accounts a ON a.Id = t.AccountId
                WHERE t.Id = @Id AND a.UserId = @UserId";

            var cmd = new CommandDefinition(sql, new { Id = id, UserId = userId }, cancellationToken: token);
            return await connection.QuerySingleOrDefaultAsync<TransactionModel>(cmd);
        }

        public async Task<bool> IsExists(Guid userId, Guid id, CancellationToken token)
        {
            using var connection = _connectionFactory.GetConnection();

            var sql = "SELECT 1 FROM Transactions WHERE Id = @Id AND UserId = @UserId";
            var cmd = new CommandDefinition(sql, new { Id = id, UserId = userId }, cancellationToken: token);
            var result = await connection.ExecuteScalarAsync<int?>(cmd);
            return result.HasValue;
        }

        public async Task<Transaction?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            var model = await _connectionFactory.ReadSingleAsync<TransactionModel, Guid>("sp_GetTransactionById", id);
            return model is null ? null : TransactionMapper.MapToDomain(model);
        }

        public async Task<TransactionDto?> GetDtoByIdAndUserId(Guid userId, Guid id, CancellationToken token = default)
        {
            var model = await GetModelByIdAndUserIdAsync(userId, id, token);
            if (model is null) return null;

            return new TransactionDto(model.Id, model.AccountId, model.ToAccountId,
                model.CategoryId, model.Amount, model.Type, model.CreatedAt, model.Description);
        }

        public async Task<Transaction?> GetByIdAndUserId(Guid userId, Guid id, CancellationToken token = default)
        {
            var model = await GetModelByIdAndUserIdAsync(userId, id, token);
            return model is null ? null : TransactionMapper.MapToDomain(model);
        }

        public async Task<IEnumerable<Transaction>> GetByAccountIdAsync(Guid accountId, CancellationToken token = default)
        {
            var models = await _connectionFactory.ReadListAsync<TransactionModel>(
                "sp_GetTransactionsByAccountId", new { AccountId = accountId });
            return models.Select(TransactionMapper.MapToDomain);
        }

        public async Task AddAsync(Transaction transaction, CancellationToken token = default)
        {
            var model = TransactionMapper.MapToModel(transaction);
            await _connectionFactory.ExecuteNonQueryAsync("sp_CreateTransaction", model);
        }

        public async Task<CreateTransferDto> TransferAsync(Transaction transaction, CancellationToken token = default)
        {
            var model = TransactionMapper.MapToModel(transaction);
            var result = await _connectionFactory.QuerySingleAsync<TransferModel>("sp_Transfer", new
            {
                SourceAccountId = model.AccountId,
                DestinationAccountId = model.ToAccountId,
                Amount = model.Amount,
                Description = model.Description
            });

            return new CreateTransferDto(result.CreditTransactionId, result.DebitTransactionId,
                model.AccountId, model.ToAccountId, model.Amount,
                result.SourceBalance, result.DestinationBalance, result.TransferDate);
        }

        public async Task<CreateTransactionDto> IncomeAsync(Transaction transaction, CancellationToken token = default)
        {
            var model = TransactionMapper.MapToModel(transaction);
            var result = await _connectionFactory.QuerySingleAsync<TransactionModel>("sp_CreateIncome", new
            {
                AccountId = model.AccountId,
                CategoryId = model.CategoryId,
                Amount = model.Amount,
                Description = model.Description
            });
            return new CreateTransactionDto(result.Id, model.AccountId, result.CategoryId,
                result.Amount, model.Type, model.Date, result.Description);
        }

        public async Task<CreateTransactionDto> ExpenseAsync(Transaction transaction, CancellationToken token = default)
        {
            var model = TransactionMapper.MapToModel(transaction);
            var result = await _connectionFactory.QuerySingleAsync<TransactionModel>("sp_CreateExpense", new
            {
                AccountId = model.AccountId,
                CategoryId = model.CategoryId,
                Amount = model.Amount,
                Description = model.Description
            });
            return new CreateTransactionDto(result.Id, model.AccountId, result.CategoryId,
                result.Amount, model.Type, model.Date, result.Description);
        }

        public async Task UpdateAsync(Transaction transaction, CancellationToken token = default)
        {
            var model = TransactionMapper.MapToModel(transaction);
            await _connectionFactory.ExecuteNonQueryAsync("sp_UpdateTransaction", model);
        }

        public async Task DeleteAsync(Guid id, CancellationToken token = default)
        {
            await _connectionFactory.ExecuteNonQueryAsync("sp_DeleteTransaction", new { id });
        }
        public async Task<decimal> GetTotalSpentAsync(Guid categoryId, DateTime start, DateTime end, byte type = 2)
        {
            var transactions = await FetchAllTransactionsAsync(categoryId, start, end, type);
            return transactions.Sum(t => t.Amount);
        }

        public async Task<ReportModel?> GetMonthlySummary(Guid accountId, DateTime start, DateTime end)
        {
            using var connection = _connectionFactory.GetConnection();

            var sql = @"
                SELECT
                    SUM(CASE WHEN TransactionTypeId = 1 THEN Amount ELSE 0 END) AS TotalIncome,
                    SUM(CASE WHEN TransactionTypeId = 2 THEN Amount ELSE 0 END) AS TotalExpenses
                FROM Transactions
                WHERE AccountId = @AccountId
                  AND Date >= @Start
                  AND Date < @End";

            var cmd = new CommandDefinition(sql, new { AccountId = accountId, Start = start, End = end });
            return await connection.QueryFirstOrDefaultAsync<ReportModel>(cmd);
        }

        public async Task<IEnumerable<TransactionDto>?> GetFiltredTransactionsAsync(
            Guid? categoryId, DateTime? start, DateTime? end, byte? type, int page, int pageSize)
        {
            return await FetchTransactionsPageAsync(null, categoryId, start, end, type, page, pageSize);
        }

        public async Task<IEnumerable<TransactionDto>?> FetchTransactionsByIdPageAsync(Guid accountId, int page, int pageSize)
        {
            return await FetchTransactionsPageAsync(accountId, null, null, null, null, page, pageSize);
        }

        private async Task<IEnumerable<TransactionDto>> FetchTransactionsPageAsync(
            Guid? accountId, Guid? categoryId, DateTime? start, DateTime? end, byte? type, int page, int pageSize)
        {
            using var connection = _connectionFactory.GetConnection();

            var sql = new StringBuilder(@"
                SELECT Id, AccountId, ToAccountId, CategoryId, Amount,
                       TransactionTypeId, Date, Description, CreatedAt, UpdatedAt
                FROM Transactions
                WHERE 1 = 1");

            if (accountId.HasValue) sql.Append(" AND AccountId    = @AccountId");
            if (categoryId.HasValue) sql.Append(" AND CategoryId   = @CategoryId");
            if (start.HasValue) sql.Append(" AND CreatedAt    >= @Start");
            if (end.HasValue) sql.Append(" AND CreatedAt    <= @End");
            if (type.HasValue) sql.Append(" AND TransactionTypeId = @Type");

            sql.Append(" ORDER BY CreatedAt OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY");

            var cmd = new CommandDefinition(sql.ToString(), new
            {
                AccountId = accountId,
                CategoryId = categoryId,
                Start = start,
                End = end,
                Type = type,
                Offset = (page - 1) * pageSize,
                PageSize = pageSize
            });

            var models = await connection.QueryAsync<TransactionModel>(cmd);
            return models.Select(m => new TransactionDto(
                m.Id, m.AccountId, m.ToAccountId, m.CategoryId,
                m.Amount, m.Type, m.CreatedAt, m.Description));
        }

        private async Task<IEnumerable<TransactionDto>> FetchAllTransactionsAsync(
            Guid? categoryId = null, DateTime? start = null, DateTime? end = null, byte? type = null)
        {
            var all = new List<TransactionDto>();
            int page = 1;
            const int pageSize = 100;

            while (true)
            {
                var batch = (await FetchTransactionsPageAsync(null, categoryId, start, end, type, page, pageSize)).ToList();
                if (!batch.Any()) break;
                all.AddRange(batch);
                page++;
            }

            return all;
        }

        public async Task<IEnumerable<SpendingByCategoryDto>> GetSpendingByCategory(
            Guid userId, Guid? accountId, DateTime start, DateTime end)
        {
            using var connection = _connectionFactory.GetConnection();

            var sql = @"
                SELECT c.Name AS Category, SUM(t.Amount) AS Amount
                FROM Transactions t
                INNER JOIN Categories c ON c.Id = t.CategoryId
                INNER JOIN Accounts   a ON a.Id = t.AccountId
                WHERE t.TransactionTypeId = 2
                  AND a.UserId = @UserId
                  AND (@AccountId IS NULL OR t.AccountId = @AccountId)
                  AND t.CreatedAt >= @Start
                  AND t.CreatedAt <  @End
                GROUP BY c.Name
                ORDER BY Amount DESC";

            var cmd = new CommandDefinition(sql, new { UserId = userId, AccountId = accountId, Start = start, End = end });
            return await connection.QueryAsync<SpendingByCategoryDto>(cmd);
        }

        public async Task<List<SpendingByCategoryDto>> GetSpendingByCategoryForUser(
            Guid userId, DateTime start, DateTime end)
        {
            using var connection = _connectionFactory.GetConnection();

            var sql = @"
                SELECT c.Name AS CategoryName, SUM(t.Amount) AS Amount
                FROM Transactions t
                INNER JOIN Accounts   a ON t.AccountId  = a.Id
                INNER JOIN Categories c ON t.CategoryId = c.Id
                WHERE a.UserId = @UserId
                  AND t.Date >= @Start
                  AND t.Date <  @End
                  AND t.TransactionTypeId = @ExpenseType
                GROUP BY c.Name
                ORDER BY Amount DESC";

            var cmd = new CommandDefinition(sql, new { UserId = userId, Start = start, End = end, ExpenseType = (byte)EnTransactionType.Expense });
            var result = await connection.QueryAsync<SpendingByCategoryDto>(cmd);
            return result.ToList();
        }
    }
}