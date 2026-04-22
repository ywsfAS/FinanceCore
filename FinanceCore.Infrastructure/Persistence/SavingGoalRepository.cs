using Dapper;
using FinanceCore.Application.Abstractions;
using FinanceCore.Application.Models;
using FinanceCore.Domain.Goals;
using FinanceCore.Infrastructure.context;
using FinanceCore.Infrastructure.Mappers;
using System.Data;

namespace FinanceCore.Infrastructure.Persistence
{
    public class SavingsGoalRepository : ISavingsGoalRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public SavingsGoalRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<SavingsGoal?> GetByIdAsync(Guid id)
        {
            using var connection = _connectionFactory.GetConnection();

            const string sql = "SELECT * FROM SavingsGoals WHERE Id = @Id";

            var model = await connection.QuerySingleOrDefaultAsync<SavingsGoalModel>(
                sql,
                new { Id = id });

            return model == null ? null : SavingsGoalMapper.MapToDomain(model);
        }


        public async Task<SavingsGoal?> GetByIdAndUserIdAsync(Guid userId,Guid id)
        {
            using var connection = _connectionFactory.GetConnection();

            const string sql = "SELECT * FROM SavingsGoals WHERE Id = @Id AND UserId = @UserId";

            var model = await connection.QuerySingleOrDefaultAsync<SavingsGoalModel>(
                sql,
                new { Id = id , UserId = userId});

            return model == null ? null : SavingsGoalMapper.MapToDomain(model);
        }

        public async Task<IEnumerable<SavingsGoal>> GetByUserIdAsync(Guid userId)
        {
            using var connection = _connectionFactory.GetConnection();

            const string sql = "SELECT * FROM SavingsGoals WHERE UserId = @UserId";

            var models = await connection.QueryAsync<SavingsGoalModel>(
                sql,
                new { UserId = userId });

            return models.Select(SavingsGoalMapper.MapToDomain);
        }

        public async Task AddAsync(SavingsGoal goal)
        {
            using var connection = _connectionFactory.GetConnection();

            const string sql = @"
            INSERT INTO SavingsGoals (
            Id,
            UserId,
            Name,
            Description,
            TargetAmount,
            CurrentAmount,
            TargetDate,
            Status,
            CreatedAt,
            UpdatedAt,
            CompletedAt
            )
            VALUES (
            @Id,
            @UserId,
            @Name,
            @Description,
            @TargetAmount,
            @CurrentAmount,
            @TargetDate,
            @Status,
            @CreatedAt,
            @UpdatedAt,
            @CompletedAt
            );";

            var model = SavingsGoalMapper.MapToModel(goal);

            await connection.ExecuteAsync(sql, model);
        }

        public async Task UpdateAsync(SavingsGoal goal)
        {
            using var connection = _connectionFactory.GetConnection();

            const string sql = @"
            UPDATE SavingsGoals
            SET
            Name = @Name,
            Description = @Description,
            TargetAmount = @TargetAmount,
            CurrentAmount = @CurrentAmount,
            TargetDate = @TargetDate,
            Status = @Status,
            UpdatedAt = @UpdatedAt,
            CompletedAt = @CompletedAt
            WHERE Id = @Id;";

            var model = SavingsGoalMapper.MapToModel(goal);

            await connection.ExecuteAsync(sql, model);
        }

        public async Task DeleteAsync(Guid id)
        {
            using var connection = _connectionFactory.GetConnection();

            const string sql = "DELETE FROM SavingsGoals WHERE Id = @Id";

            await connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            using var connection = _connectionFactory.GetConnection();

            const string sql = "SELECT 1 FROM SavingsGoals WHERE Id = @Id";

            var result = await connection.ExecuteScalarAsync<int?>(
                sql,
                new { Id = id });

            return result.HasValue;
        }
    }
}
