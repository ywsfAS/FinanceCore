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

        public async Task<TransactionDto?> GetDtoByIdAndUserId(Guid userId,Guid id,CancellationToken token = default)
        {
            using var connection = _connectionFactory.GetConnection();

            const string sql = @"
            SELECT
            t.Id,
            a.Name  AS AccountName,
            ta.Name AS ToAccountName,
            c.Name  AS CategoryName,
            t.Amount,
            a.CurrencyId AS Currency,
            t.TransactionTypeId AS Type,
            t.CreatedAt AS Date,
            t.Description
            FROM Transactions t
            INNER JOIN Accounts a
                ON t.AccountId = a.Id
            LEFT JOIN Accounts ta
                ON t.ToAccountId = ta.Id
            LEFT JOIN Categories c
                ON t.CategoryId = c.Id
            WHERE t.Id = @Id
                AND a.UserId = @UserId";

            return await connection.QueryFirstOrDefaultAsync<TransactionDto>(
                new CommandDefinition(
                    sql,
                    new
                    {
                        Id = id,
                        UserId = userId
                    },
                    cancellationToken: token));
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
                    COALESCE(SUM(CASE WHEN TransactionTypeId = 0 THEN Amount ELSE 0 END),0) AS TotalIncome,
                    COALESCE(SUM(CASE WHEN TransactionTypeId = 1 THEN Amount ELSE 0 END),0) AS TotalExpense
                FROM Transactions
                WHERE AccountId = @AccountId
                  AND CreatedAt >= @Start
                  AND CreatedAt < @End";

            var cmd = new CommandDefinition(sql, new { AccountId = accountId, Start = start, End = end });
            return await connection.QueryFirstOrDefaultAsync<ReportModel>(cmd);
        }
       public async Task<ReportModel?> GetSummaryByUser(Guid userId,CancellationToken token)
        {
            using var connection = _connectionFactory.GetConnection();
            const string sql = @"
                SELECT 
                    COALESCE(SUM(CASE WHEN TransactionTypeId = 0 THEN Amount ELSE 0 END),0) AS TotalIncome,
                    COALESCE(SUM(CASE WHEN TransactionTypeId = 1 THEN Amount ELSE 0 END),0) AS TotalExpense
                FROM Transactions t
                INNER JOIN Accounts a ON
                    t.AccountId = a.Id
                WHERE a.UserId = @UserId 
            ";
            var command = new CommandDefinition(sql, new { UserId = userId} , cancellationToken : token);
            return await connection.QueryFirstOrDefaultAsync<ReportModel>(command);
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
            SELECT
            t.Id,
            a.Name  AS AccountName,
            ta.Name AS ToAccountName,
            c.Name  AS CategoryName,
            t.Amount,
            a.CurrencyId AS Currency,
            t.TransactionTypeId AS Type,
            t.CreatedAt AS Date,
            t.Description 
            FROM Transactions t
            INNER JOIN Accounts a
                ON t.AccountId = a.Id
            LEFT JOIN Accounts ta
                ON t.ToAccountId = ta.Id
            LEFT JOIN Categories c
                ON t.CategoryId = c.Id
            WHERE 1 = 1");
            if (accountId.HasValue) sql.Append(" AND t.AccountId    = @AccountId");
            if (categoryId.HasValue) sql.Append(" AND t.CategoryId   = @CategoryId");
            if (start.HasValue) sql.Append(" AND t.CreatedAt    >= @Start");
            if (end.HasValue) sql.Append(" AND t.CreatedAt    <= @End");
            if (type.HasValue) sql.Append(" AND t.TransactionTypeId = @Type"); 
            sql.Append(" ORDER BY t.CreatedAt OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY");

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

            return await connection.QueryAsync<TransactionDto>(cmd);
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
                WHERE t.TransactionTypeId = 1
                  AND a.UserId = @UserId
                  AND (@AccountId IS NULL OR t.AccountId = @AccountId)
                  AND t.CreatedAt >= @Start
                  AND t.CreatedAt <  @End
                GROUP BY c.Name
                ORDER BY Amount DESC";

            var cmd = new CommandDefinition(sql, new { UserId = userId, AccountId = accountId, Start = start, End = end });
            var models = await connection.QueryAsync<SpendingByCategoryModel>(cmd);
            return models.Select(model => new SpendingByCategoryDto(model.Category, model.Amount));
        }

        public async Task<IEnumerable<SpendingByCategoryDto>> GetSpendingByCategoryForUser(
            Guid userId, DateTime start, DateTime end)
        {
            using var connection = _connectionFactory.GetConnection();

            var sql = @"
                SELECT c.Name AS Category, SUM(t.Amount) AS Amount
                FROM Transactions t
                INNER JOIN Accounts   a ON t.AccountId  = a.Id
                INNER JOIN Categories c ON t.CategoryId = c.Id
                WHERE a.UserId = @UserId
                  AND t.CreatedAt >= @Start
                  AND t.CreatedAt <  @End
                  AND t.TransactionTypeId = @ExpenseType
                GROUP BY c.Name
                ORDER BY Amount DESC";

            var cmd = new CommandDefinition(sql, new { UserId = userId, Start = start, End = end, ExpenseType = (byte)EnTransactionType.Expense });
            var models = await connection.QueryAsync<SpendingByCategoryModel>(cmd);
            return models.Select(model => new SpendingByCategoryDto(model.Category, model.Amount));
        }
        public async Task<ReportModel?> GetMonthlySumaryByUser(Guid userId, DateTime start, DateTime end, CancellationToken token)
        {

            using var connection = _connectionFactory.GetConnection();
            const string sql = @"
                SELECT 
                    COALESCE(SUM(CASE WHEN TransactionTypeId = 0 THEN Amount ELSE 0 END),0) AS TotalIncome,
                    COALESCE(SUM(CASE WHEN TransactionTypeId = 1 THEN Amount ELSE 0 END),0) AS TotalExpense
                FROM Transactions t
                INNER JOIN Accounts a ON
                    t.AccountId = a.Id
                WHERE a.UserId = @UserId AND t.CreatedAt >= @Start AND t.CreatedAt < @End 
            ";
            var command = new CommandDefinition(sql, new { UserId = userId,Start = start , End = end} , cancellationToken : token);
            return await connection.QueryFirstOrDefaultAsync<ReportModel>(command);

        }
        public async Task<IEnumerable<MonthlyTrendDto>> GetMonthlyTrend(Guid UserId, int months)
        {
            using var connection = _connectionFactory.GetConnection();

            const string sql = @"
            WITH Months AS (
            SELECT 
                DATEFROMPARTS(
                    YEAR(DATEADD(MONTH, -v.number, GETDATE())),
                    MONTH(DATEADD(MONTH, -v.number, GETDATE())),
                    1
            ) AS MonthDate
            FROM master.dbo.spt_values v
            WHERE v.type = 'P'
            AND v.number < @Months
            ),
            TransactionsGrouped AS (
            SELECT
                DATEFROMPARTS(YEAR(t.CreatedAt), MONTH(t.CreatedAt), 1) AS MonthDate,
                SUM(CASE WHEN t.TransactionTypeId = 1 THEN t.Amount ELSE 0 END) AS TotalIncome,
                SUM(CASE WHEN t.TransactionTypeId = 2 THEN t.Amount ELSE 0 END) AS TotalExpense

            FROM Transactions t
            INNER JOIN Accounts a ON t.AccountId = a.Id
            WHERE a.UserId = @UserId
            AND t.CreatedAt >= DATEADD(MONTH, -@Months, GETDATE())
            GROUP BY YEAR(t.CreatedAt), MONTH(t.CreatedAt))
            SELECT 
                FORMAT(m.MonthDate, 'MMM') AS Month,
                ISNULL(t.TotalIncome, 0) AS TotalIncome,
                ISNULL(t.TotalExpense, 0) AS TotalExpense
            FROM Months m
            LEFT JOIN TransactionsGrouped t
            ON m.MonthDate = t.MonthDate
            ORDER BY m.MonthDate;

            ";

            return await connection.QueryAsync<MonthlyTrendDto>(
                sql,
                new { UserId = UserId, Months = months }
            );
        }

    }
}