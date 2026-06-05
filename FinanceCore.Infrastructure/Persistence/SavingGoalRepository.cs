using Dapper;
using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs.Goal;
using FinanceCore.Application.Models;
using FinanceCore.Domain.Enums;
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

        public async Task<SavingsGoal?> GetByIdAsync(Guid id, CancellationToken token)
        {
            using var connection = _connectionFactory.GetConnection();

            const string sql = "SELECT * FROM SavingsGoals WHERE Id = @Id";

            var model = await connection.QuerySingleOrDefaultAsync<SavingsGoalModel>(
                sql,
                new { Id = id });

            return model == null ? null : SavingsGoalMapper.MapToDomain(model);
        }


        public async Task<SavingsGoal?> GetByIdAndUserIdAsync(Guid userId,Guid id, CancellationToken token)
        {
            using var connection = _connectionFactory.GetConnection();

            const string sql = "SELECT * FROM SavingsGoals WHERE Id = @Id AND UserId = @UserId";

            var model = await connection.QuerySingleOrDefaultAsync<SavingsGoalModel>(
                sql,
                new { Id = id , UserId = userId});

            return model == null ? null : SavingsGoalMapper.MapToDomain(model);
        }
        public async Task<IEnumerable<SavingsGoal>> GetByUserIdAsync(Guid userId, int page, int pageSize, CancellationToken token)
        {
            using var connection = _connectionFactory.GetConnection();

            int skip = (page - 1) * pageSize;

            const string sql = @"SELECT * FROM SavingsGoals 
                        WHERE UserId = @UserId
                        ORDER BY Id 
                        OFFSET @Skip ROWS 
                        FETCH NEXT @PageSize ROWS ONLY;";

            var command = new CommandDefinition(
                sql,
                new { UserId = userId, Skip = skip, PageSize = pageSize },
                cancellationToken: token
            );

            var models = await connection.QueryAsync<SavingsGoalModel>(command);

            return models.Select(SavingsGoalMapper.MapToDomain);
        }


        private async Task<IEnumerable<SavingsGoalModel>?> GetModelsByUserIdAndStatusAsync(Guid userId,EnGoalStatus status,int page,int pageSize , CancellationToken token)
        {
            using var connection = _connectionFactory.GetConnection();

            int skip = (page - 1) * pageSize;
            const string sql = @"SELECT * FROM SavingsGoals 
                        WHERE UserId = @UserId
                        AND StatusId = @StatusId
                        ORDER BY Id 
                        OFFSET @Skip ROWS 
                        FETCH NEXT @PageSize ROWS ONLY;";

            var command = new CommandDefinition(
                sql,
                new { UserId = userId, Skip = skip, PageSize = pageSize , StatusId = (byte)status},
                cancellationToken: token
            );

            var models = await connection.QueryAsync<SavingsGoalModel>(command);

            return models;
        }
        public async Task<IEnumerable<SavingsGoalDto>?> GetDtosByUserIdAndStatusAsync(Guid userId , EnGoalStatus status , int page , int pageSize , CancellationToken token)
        {
            var models = await GetModelsByUserIdAndStatusAsync(userId, status, page, pageSize, token);
            return models.Select(model => new SavingsGoalDto(model.Id,model.UserId,model.Name,model.Description,model.TargetAmount,model.CurrentAmount,(EnCurrency)model.CurrencyId,model.TargetDate,(EnGoalStatus)model.StatusId,model.CreatedAt,model.UpdatedAt,model.CompletedAt));
        }
        public async Task AddAsync(SavingsGoal goal , CancellationToken token)
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
            CurrencyId,
            TargetDate,
            StatusId,
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
            @CurrencyId,
            @TargetDate,
            @StatusId,
            @CreatedAt,
            @UpdatedAt,
            @CompletedAt
            );";
            var model = SavingsGoalMapper.MapToModel(goal);
            var command = new CommandDefinition(sql,model, cancellationToken : token);
            await connection.ExecuteAsync(command);
        }

        public async Task UpdateAsync(SavingsGoal goal,CancellationToken token)
        {
            using var connection = _connectionFactory.GetConnection();

            const string sql = @"
            UPDATE SavingsGoals
            SET
            Name = @Name,
            Description = @Description,
            TargetAmount = @TargetAmount,
            CurrentAmount = @CurrentAmount,
            CurrencyId = @CurrencyId
            TargetDate = @TargetDate,
            StatusId = @StatusId,
            UpdatedAt = @UpdatedAt,
            CompletedAt = @CompletedAt
            WHERE Id = @Id;";

            var model = SavingsGoalMapper.MapToModel(goal);
            var command = new CommandDefinition(sql,model,cancellationToken: token);
            await connection.ExecuteAsync(command);
        }

        public async Task DeleteAsync(Guid id, CancellationToken token)
        {
            using var connection = _connectionFactory.GetConnection();

            const string sql = "DELETE FROM SavingsGoals WHERE Id = @Id";

            await connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken token)
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
