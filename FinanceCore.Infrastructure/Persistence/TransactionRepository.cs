using Dapper;
using Z.Dapper.Plus;
using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.DTOs.Transaction;
using FinanceCore.Application.Models;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Transactions;
using FinanceCore.Infrastructure.Context;
using FinanceCore.Infrastructure.Mappers;
using System.Text;
using System.Data.Common;

namespace FinanceCore.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public TransactionRepository(IConnectionFactory connectionFactory )
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
        public async Task AddAsync( Transaction transaction,IUnitOfWork? unitOfWork = null,CancellationToken token = default)
        {
            var sql = """
        INSERT INTO Transactions
        (
            Id,
            AccountId,
            ToAccountId,
            CategoryId,
            Amount,
            CurrencyId,
            TransactionTypeId,
            Date,
            CreatedAt,
            UpdatedAt,
            Description
        )
        VALUES
        (
            @Id,
            @AccountId,
            @ToAccountId,
            @CategoryId,
            @Amount,
            @Currency,
            @Type,
            @Date,
            @CreatedAt,
            @UpdatedAt,
            @Description
        );
        """;

            var model = TransactionMapper.MapToModel(transaction);

            if (unitOfWork is not null)
            {
                var cmd = new CommandDefinition(
                    sql,
                    model,
                    unitOfWork.Transaction,
                    cancellationToken: token);

                await unitOfWork.Connection!.ExecuteAsync(cmd);

                return;
            }

            using var connection = _connectionFactory.GetConnection();

            var command = new CommandDefinition(
                sql,
                model,
                cancellationToken: token);

            await connection.ExecuteAsync(command);
        }
        public async Task<bool> IsExistsAsync(Guid userId, Guid id, CancellationToken token)
        {
            using var connection = _connectionFactory.GetConnection();

            var sql = "SELECT 1 FROM Transactions WHERE Id = @Id AND UserId = @UserId";
            var cmd = new CommandDefinition(sql, new { Id = id, UserId = userId }, cancellationToken: token);
            var result = await connection.ExecuteScalarAsync<int?>(cmd);
            return result.HasValue;
        }
        public async Task<TransactionDto?> GetDtoByIdAndUserId(Guid userId,Guid id,CancellationToken token = default)
        {
            using var connection = _connectionFactory.GetConnection();

            const string sql = @"
            SELECT
            t.Id,
            t.CurrencyId AS Currency,
            a.Name  AS AccountName,
            ta.Name AS ToAccountName,
            c.Name  AS CategoryName,
            t.Amount,
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
        public async Task DeleteAsync(Guid userId , Guid id, CancellationToken token = default)
        {
            using var connection = _connectionFactory.GetConnection();
            const string sql = @"DELETE t FROM Transactions t
            INNER JOIN Accounts a 
            ON t.AccountId = a.Id
            WHERE a.UserId = @UserId AND t.Id = @Id";
            var command = new CommandDefinition(sql, new {Id = id , UserId = userId} , cancellationToken : token); 
            await connection.ExecuteAsync(command);
        }
        //passed
        public async Task<IEnumerable<MonthlySummaryDto>> GetMonthlySummaryAsync(
            Guid userId,
            Guid? accountId,
            DateTime start,
            DateTime end,
            int page,
            int pageSize,
            CancellationToken token)
        {
            using var connection = _connectionFactory.GetConnection();

            var sql = new StringBuilder(@"
            SELECT
            t.AccountId,

            COALESCE(SUM(CASE WHEN t.TransactionTypeId = 0
                THEN t.Amount * er.Rate ELSE 0 END), 0) AS TotalIncome,

            COALESCE(SUM(CASE WHEN t.TransactionTypeId = 1
                THEN t.Amount * er.Rate ELSE 0 END), 0) AS TotalExpense,

            (
            COALESCE(SUM(CASE WHEN t.TransactionTypeId = 0
                THEN t.Amount * er.Rate ELSE 0 END), 0)
            -
            COALESCE(SUM(CASE WHEN t.TransactionTypeId = 1
                THEN t.Amount * er.Rate ELSE 0 END), 0)
            ) AS NetSavings,

            p.Currency AS Currency

            FROM Transactions t

            INNER JOIN Accounts a 
                ON a.Id = t.AccountId  
                AND t.Date >= @Start
                AND t.Date < @End

            INNER JOIN Profiles p
                ON p.UserId = @UserId

            INNER JOIN ExchangeRates er
                ON er.SourceCurrencyId = a.CurrencyId
                AND er.TargetCurrencyId = p.Currency

            WHERE a.UserId = @UserId
            ");

            if (accountId.HasValue)
                sql.Append(" AND t.AccountId = @AccountId ");

            sql.Append(@"
            GROUP BY
                t.AccountId,
                p.Currency

            ORDER BY t.AccountId
            OFFSET @Offset ROWS
            FETCH NEXT @PageSize ROWS ONLY
            ");

            var cmd = new CommandDefinition(
                sql.ToString(),
                new
                {
                    UserId = userId,
                    AccountId = accountId,
                    Start = start,
                    End = end,
                    Offset = (page - 1) * pageSize,
                    PageSize = pageSize
                },
                cancellationToken: token);

            return await connection.QueryAsync<MonthlySummaryDto>(cmd);
        }
        // passed
        public async Task<IEnumerable<TransactionDto>> GetFilteredTransactionsAsync(
            Guid userId,Guid? accountId,Guid? toAccountId, Guid? categoryId, DateTime? start, DateTime? end, EnTransactionType? type, int page, int pageSize , CancellationToken token)
        {
            using var connection = _connectionFactory.GetConnection();

            var sql = new StringBuilder(@"
            SELECT
            t.Id,
            a.Name  AS AccountName,
            ta.Name AS ToAccountName,
            c.Name  AS CategoryName,
            t.Amount,
            t.CurrencyId AS Currency,
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
            WHERE a.UserId = @UserId");
            if (accountId.HasValue) sql.Append(" AND t.AccountId    = @AccountId");
            if (toAccountId.HasValue) sql.Append(" AND t.ToAccountId = @ToAccountId");
            if (categoryId.HasValue) sql.Append(" AND t.CategoryId   = @CategoryId");
            if (start.HasValue) sql.Append(" AND t.Date    >= @Start");
            if (end.HasValue) sql.Append(" AND t.Date    <= @End");
            if (type.HasValue) sql.Append(" AND t.TransactionTypeId = @Type"); 
            sql.Append(" ORDER BY t.CreatedAt OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY");

            var cmd = new CommandDefinition(sql.ToString(), new
            {
                UserId = userId,
                ToAccountId = toAccountId,
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
        // passed
        public async Task<IEnumerable<SpendingByCategoryDto>> GetSpendingByCategoryAsync(
            Guid userId, Guid? accountId, DateTime start, DateTime end,int page , int pageSize ,CancellationToken token)
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
                ORDER BY Amount DESC 
                OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY
                ";

            var cmd = new CommandDefinition(sql, new { UserId = userId, AccountId = accountId, Start = start, End = end , Offset = (page - 1) * pageSize , pageSize = pageSize },cancellationToken : token);
            return await connection.QueryAsync<SpendingByCategoryDto>(cmd);
        }
        public async Task<decimal> GetTotalSpendingByCategoryAsync(
        Guid userId,
        Guid categoryId,
        DateTime start,
        DateTime end,
        CancellationToken token)
        {
            using var connection = _connectionFactory.GetConnection();

            var sql = @"
        SELECT COALESCE(SUM(t.Amount), 0)
        FROM Transactions t
        INNER JOIN Accounts a ON a.Id = t.AccountId
        WHERE a.UserId = @UserId
          AND t.CategoryId = @CategoryId
          AND t.TransactionTypeId = 1
          AND t.Date >= @Start
          AND t.Date <= @End;
    ";

            var cmd = new CommandDefinition(sql, new
            {
                UserId = userId,
                CategoryId = categoryId,
                Start = start,
                End = end
            }, cancellationToken: token);

            return await connection.QueryFirstOrDefaultAsync<decimal>(cmd);
        }

        public async Task<IEnumerable<BudgetHealthDataDto>?> GetBudgetHealthAsync(Guid userId , int page ,int pageSize , CancellationToken token = default)
        {
            using var connection = _connectionFactory.GetConnection();
            var sql = new StringBuilder(@"
            SELECT 
	            b.Id,
	            b.Name,
	            b.Amount,
                b.CurrencyId AS Currency,
	            COALESCE(SUM(t.Amount),0) AS Spent,
	        CASE
		        WHEN b.Amount = 0 THEN 0
		        ELSE COALESCE(SUM(t.Amount),0) / b.Amount
	        END
	            AS UsagePercentage
            FROM Transactions t
            INNER JOIN Categories c
	            ON t.CategoryId = c.Id
            INNER JOIN Budgets b
	            ON b.CategoryId = c.Id 
	        WHERE c.UserId = @UserId
            GROUP BY b.Id,
		        b.Name,
		        b.Amount,
                b.CurrencyId
            Order BY b.Amount
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
            ");
            var command = new CommandDefinition(sql.ToString(),new {UserId = userId , Offset = (page - 1) * pageSize , PageSize = pageSize } , cancellationToken : token);
            return await connection.QueryAsync<BudgetHealthDataDto>(command);
        }

        // passed
        public async Task<IEnumerable<MonthlyTrendDto>> GetMonthlyTrend(Guid userId, int lastNMonth ,CancellationToken token)
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
                SUM(CASE WHEN t.TransactionTypeId = 0 THEN t.Amount * er.Rate ELSE 0 END) AS TotalIncome,
                SUM(CASE WHEN t.TransactionTypeId = 1 THEN t.Amount * er.Rate ELSE 0 END) AS TotalExpense,
                (SUM(CASE WHEN t.TransactionTypeId = 0 THEN t.Amount * er.Rate ELSE 0 END)
                -
                SUM(CASE WHEN t.TransactionTypeId = 1 THEN t.Amount * er.Rate ELSE 0 END)) AS NetSavings
                
            FROM Transactions t
            INNER JOIN Accounts a ON t.AccountId = a.Id
            INNER JOIN Profiles p ON a.UserId = p.UserId
            INNER JOIN ExchangeRates er ON er.SourceCurrencyId = a.CurrencyId AND er.TargetCurrencyId = p.Currency
            WHERE a.UserId = @UserId
             
            AND t.CreatedAt >= DATEADD(MONTH, -@Months, GETDATE())
            GROUP BY YEAR(t.CreatedAt), MONTH(t.CreatedAt))
            SELECT 
                FORMAT(m.MonthDate, 'MMM') AS Month,
                ISNULL(t.TotalIncome, 0) AS TotalIncome,
                ISNULL(t.TotalExpense, 0) AS TotalExpense,
                ISNULL(t.NetSavings,0) AS NetSavings,
                (SELECT TOP 1 Currency FROM Profiles WHERE UserId = @UserId) AS Currency
            FROM Months m
            LEFT JOIN TransactionsGrouped t
            ON m.MonthDate = t.MonthDate
            ORDER BY m.MonthDate;

            ";

            return await connection.QueryAsync<MonthlyTrendDto>(
                sql,
                new { UserId = userId, Months = lastNMonth } 
              
            );
        }
        public async Task InsertTransactions(IEnumerable<Transaction> transactions,IUnitOfWork? unitOfWork = null, CancellationToken token = default)
        {
            var transactionModels = new List<TransactionModel>();
            foreach(var transaction in transactions)
            {
               transactionModels.Add(TransactionMapper.MapToModel(transaction));
            }
            if(unitOfWork is not null)
            {
                await unitOfWork.Connection.UseBulkOptions(options =>
                {
                    options.Transaction = (DbTransaction)unitOfWork.Transaction;
                    options.CancellationToken = token;
                })
                .BulkInsertAsync(transactionModels);
                return;
            }
            using var connection = _connectionFactory.GetConnection();
            await connection.BulkInsertAsync(transactionModels);
        }

    }
}