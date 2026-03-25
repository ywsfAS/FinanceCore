using Dapper;
using FinanceCore.Application.Abstractions;
using FinanceCore.Application.Models;
using FinanceCore.Domain.RecurringTransaction;
using FinanceCore.Infrastructure.Mappers;
using FinanceCore.Infrastructure.context;
using System.Data;

namespace FinanceCore.Infrastructure.Persistence
{
    public class RecurringTransactionRepository : IRecurringTransactionRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public RecurringTransactionRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<RecurringTransaction?> GetByIdAsync(Guid id)
        {
            using var connection = _connectionFactory.GetConnection();
            var sql = "SELECT * FROM RecurringTransactions WHERE Id = @Id";
            var model = await connection.QuerySingleOrDefaultAsync<RecurringTransactionModel>(sql, new { Id = id });
            return model == null ? null : RecurringTransactionMapper.MapToDomain(model);
        }

        public async Task AddAsync(RecurringTransaction recurringTransaction)
        {
            var model = RecurringTransactionMapper.MapToModel(recurringTransaction);
            const string sql = @"
            INSERT INTO RecurringTransactions (
            Id,
            AccountId,
            CategoryId,
            Amount,
            Description,
            Type,
            StartDate,
            EndDate,
            Period,
            Interval,
            IsActive,
            LastExecutedDate
            )
            VALUES (
            @Id,
            @AccountId,
            @CategoryId,
            @Amount,
            @Description,
            @Type,
            @StartDate,
            @EndDate,
            @Period,
            @Interval,
            @IsActive,
            @LastExecutedDate
            );";

            using var connection = _connectionFactory.GetConnection();
            await connection.ExecuteAsync(sql, model);
        }

        public async Task UpdateAsync(RecurringTransaction recurringTransaction)
        {
            var model = RecurringTransactionMapper.MapToModel(recurringTransaction);
            const string sql = @"
            UPDATE RecurringTransactions
            SET
            AccountId = @AccountId,
            CategoryId = @CategoryId,
            Amount = @Amount,
            Description = @Description,
            Type = @Type,
            StartDate = @StartDate,
            EndDate = @EndDate,
            Period = @Period,
            Interval = @Interval,
            IsActive = @IsActive,
            LastExecutedDate = @LastExecutedDate
            WHERE Id = @Id;";

            using var connection = _connectionFactory.GetConnection();
            await connection.ExecuteAsync(sql, model);
        }

        public async Task DeleteAsync(Guid id)
        {
            using var connection = _connectionFactory.GetConnection();
            const string sql = "DELETE FROM RecurringTransactions WHERE Id = @Id";
            await connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<IEnumerable<RecurringTransaction>> GetActiveAsync()
        {
            using var connection = _connectionFactory.GetConnection();
            const string sql = "SELECT * FROM RecurringTransactions WHERE IsActive = 1";
            var models = await connection.QueryAsync<RecurringTransactionModel>(sql);
            return models.Select(RecurringTransactionMapper.MapToDomain);
        }

        public async Task<IEnumerable<RecurringTransaction>> GetByAccountAsync(Guid accountId)
        {
            using var connection = _connectionFactory.GetConnection();
            const string sql = "SELECT * FROM RecurringTransactions WHERE AccountId = @AccountId";
            var models = await connection.QueryAsync<RecurringTransactionModel>(sql, new { AccountId = accountId });
            return models.Select(RecurringTransactionMapper.MapToDomain);
        }

        public async Task<IEnumerable<RecurringTransaction>> GetByCategoryAsync(Guid categoryId)
        {
            using var connection = _connectionFactory.GetConnection();
            const string sql = "SELECT * FROM RecurringTransactions WHERE CategoryId = @CategoryId";
            var models = await connection.QueryAsync<RecurringTransactionModel>(sql, new { CategoryId = categoryId });
            return models.Select(RecurringTransactionMapper.MapToDomain);
        }

        public async Task<IEnumerable<RecurringTransaction>> GetDueTransactionsAsync(DateTime currentDate)
        {
            var active = await GetActiveAsync();
            return active.Where(r => r.CanExecute(currentDate));
        }
    }
}
