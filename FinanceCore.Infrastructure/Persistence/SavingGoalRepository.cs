using Dapper;
using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs.Goal;
using FinanceCore.Application.Models;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Goals;
using FinanceCore.Infrastructure.context;
using FinanceCore.Infrastructure.Mappers;
using System.Data;
using System.Text;

namespace FinanceCore.Infrastructure.Persistence
{
    public class SavingsGoalRepository : ISavingsGoalRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public SavingsGoalRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<SavingsGoal?> GetGoalByIdAsync(Guid id, CancellationToken token)
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
        public async Task<IEnumerable<SavingsGoalDto>> GetSavingGoalsFilteredAsync(Guid userId,string? name , EnCurrency? currency , EnGoalStatus? status, int page, int pageSize, CancellationToken token)
        {
            using var connection = _connectionFactory.GetConnection();

            int skip = (page - 1) * pageSize;

            var sql = new StringBuilder(@"SELECT * FROM SavingsGoals WHERE UserId = @UserId");
            if (name is not null) sql.Append(" AND Name LIKE @Name");
            if (currency.HasValue) sql.Append(" AND CurrencyId = @Currency");
            if (status.HasValue) sql.Append(" AND StatusId = @Status");
            sql.Append(" OFFSET @Skip ROWS FETCH NEXT @PageSize ROWS ONLY;");

            var command = new CommandDefinition(
                sql.ToString(),
                new { UserId = userId, Skip = skip, PageSize = pageSize , Name = $"%{name}%" , Currency = currency , Status = status },
                cancellationToken: token
            );

            var models = await connection.QueryAsync<SavingsGoalModel>(command);

            return models.Select((m) => new SavingsGoalDto(m.Id,m.UserId,m.Name,m.Description,m.TargetAmount,m.CurrentAmount,(EnCurrency)m.CurrencyId,m.TargetDate,(EnGoalStatus)m.StatusId,m.CompletedAt));
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
            CurrencyId = @CurrencyId,
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

        public async Task<bool> IsExistsAsync(Guid id, CancellationToken token)
        {
            using var connection = _connectionFactory.GetConnection();

            const string sql = "SELECT 1 FROM SavingsGoals WHERE Id = @Id";

            var result = await connection.ExecuteScalarAsync<int?>(
                sql,
                new { Id = id });

            return result.HasValue;

        }

        public async Task AddContribution(SavingsContribution contribution, CancellationToken token)
        {
            using var connection = _connectionFactory.GetConnection();
            const string sql = @"INSERT INTO Contributions(
             Id,
             SavingGoalId,
             AccountId,
             Amount,
             Type,
             CurrencyId,
             Description,
             Date
             )
             VALUES(
             @Id,
             @SavingGoalId,
             @AccountId,
             @Amount,
             @Type,
             @CurrencyId,
             @Description,
             @Date
             )
            ";
            var command = new CommandDefinition(sql, new
            {
                Id = contribution.Id,
                SavingGoalId = contribution.SavingGoalId,
                AccountId = contribution.AccountId,
                Amount = contribution.Amount.Amount,
                CurrencyId = contribution.Amount.Currency,
                Type = contribution.Type,
                Description = contribution.Description,
                Date = contribution.Date
            },cancellationToken: token);
            await connection.ExecuteAsync(command);
        }
    }
}
