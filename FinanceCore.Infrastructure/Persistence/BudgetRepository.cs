using Dapper;
using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.Models;
using FinanceCore.Domain.Budgets;
using FinanceCore.Domain.Enums;
using FinanceCore.Infrastructure.Mappers;
using FinanceCore.Infrastructure.context;
using System.Text;

namespace FinanceCore.Infrastructure.Repositories
{
    public class BudgetRepository : IBudgetRepository
    {
        private readonly IConnectionFactory  _connectionFactory;

        public BudgetRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        // Check if a budget exists for a user
        public async Task<bool> IsExists(Guid userId, Guid id, CancellationToken token)
        {
            using var connection = _connectionFactory.GetConnection();

            const string sql = @"
                SELECT 1
                FROM Budgets
                WHERE Id = @Id AND UserId = @UserId";

            var cmd = new CommandDefinition(sql, new { Id = id, UserId = userId }, cancellationToken: token);

            var result = await connection.ExecuteScalarAsync<int?>(cmd);

            return result.HasValue;
        }

        // Get budget by Id
        public async Task<Budget?> GetByIdAsync(Guid id, CancellationToken token)
        {
            using var connection = _connectionFactory.GetConnection();

            const string sql = @"
                SELECT *
                FROM Budgets
                WHERE Id = @Id";

            var cmd = new CommandDefinition(sql, new { Id = id }, cancellationToken: token);

            var model = await connection.QuerySingleOrDefaultAsync<BudgetModel>(cmd);

            return model is null ? null : BudgetMapper.MapToDomain(model);
        }

        // Get budget overlapping a category period
        public async Task<BudgetDto?> GetByCategoryIdAsync(Guid userId, Guid categoryId, DateTime start, DateTime end, CancellationToken token)
        {
            using var connection = _connectionFactory.GetConnection();

            const string sql = @"
                SELECT TOP 1 *
                FROM Budgets
                WHERE UserId = @UserId
                  AND CategoryId = @CategoryId
                  AND StartDate <= @End
                  AND EndDate >= @Start
                ORDER BY StartDate DESC";

            var cmd = new CommandDefinition(sql, new
            {
                UserId = userId,
                CategoryId = categoryId,
                Start = start,
                End = end
            }, cancellationToken: token);

            var model = await connection.QuerySingleOrDefaultAsync<BudgetModel>(cmd);

            return model is null ? null : MapToDto(model);
        }

        // Get all budgets for a user
        public async Task<IEnumerable<Budget>> GetByUserIdAsync(Guid userId, CancellationToken token)
        {
            using var connection = _connectionFactory.GetConnection();

            const string sql = @"
                SELECT *
                FROM Budgets
                WHERE UserId = @UserId";

            var cmd = new CommandDefinition(sql, new { UserId = userId }, cancellationToken: token);

            var models = await connection.QueryAsync<BudgetModel>(cmd);

            return models.Select(BudgetMapper.MapToDomain);
        }

        // Get all budgets as DTOs
        public async Task<IEnumerable<BudgetDto>> GetDtoByUserIdAsync(Guid userId, CancellationToken token)
        {
            using var connection = _connectionFactory.GetConnection();

            const string sql = @"
                SELECT *
                FROM Budgets
                WHERE UserId = @UserId";

            var cmd = new CommandDefinition(sql, new { UserId = userId }, cancellationToken: token);

            var models = await connection.QueryAsync<BudgetModel>(cmd);

            return models.Select(MapToDto);
        }

        // Insert new budget
        public async Task AddAsync(Budget budget, CancellationToken token)
        {
            using var connection = _connectionFactory.GetConnection();

            const string sql = @"
                INSERT INTO Budgets (
                    Id, UserId, CategoryId, Amount, CurrencyId,
                    BudgetPeriodId, StartDate, EndDate,
                    CreatedAt, UpdatedAt, Name
                )
                VALUES (
                    @Id, @UserId, @CategoryId, @Amount, @CurrencyId,
                    @BudgetPeriodId, @StartDate, @EndDate,
                    @CreatedAt, @UpdatedAt, @Name
                )";

            var model = BudgetMapper.MapToModel(budget);

            var cmd = new CommandDefinition(sql, model, cancellationToken: token);

            await connection.ExecuteAsync(cmd);
        }

        // Update existing budget
        public async Task UpdateAsync(Budget budget, CancellationToken token)
        {
            using var connection = _connectionFactory.GetConnection();

            const string sql = @"
                UPDATE Budgets
                SET
                    CategoryId = @CategoryId,
                    Amount = @Amount,
                    CurrencyId = @CurrencyId,
                    BudgetPeriodId = @BudgetPeriodId,
                    StartDate = @StartDate,
                    EndDate = @EndDate,
                    UpdatedAt = @UpdatedAt,
                    Name = @Name
                WHERE Id = @Id AND UserId = @UserId";

            var model = BudgetMapper.MapToModel(budget);

            var cmd = new CommandDefinition(sql, model, cancellationToken: token);

            await connection.ExecuteAsync(cmd);
        }

        // Delete budget
        public async Task DeleteAsync(Guid id, CancellationToken token)
        {
            using var connection = _connectionFactory.GetConnection();

            const string sql = @"
                DELETE FROM Budgets
                WHERE Id = @Id";

            var cmd = new CommandDefinition(sql, new { Id = id }, cancellationToken: token);

            await connection.ExecuteAsync(cmd);
        }

        public async Task<IEnumerable<BudgetInfoDto>?> GetBudgetsFilteredAsync(
            Guid userId,
            string? name,
            Guid? categoryId,
            EnPeriod? period,
            int page = 1,
            int pageSize = 10,
            CancellationToken token = default)
        {
            using var connection = _connectionFactory.GetConnection();

            var sql = new StringBuilder(@"
            SELECT 
            b.Id,
            b.Name,
            b.Amount,
            b.CurrencyId AS Currency,
            b.BudgetPeriodId AS Period,
            b.StartDate,
            b.EndDate,
            c.Name AS CategoryName
            FROM Budgets b
            INNER JOIN Categories c ON c.Id = b.CategoryId
            WHERE b.UserId = @UserId
            ");

            if (!string.IsNullOrWhiteSpace(name))
                sql.Append(" AND b.Name LIKE @Name");

            if (categoryId.HasValue)
                sql.Append(" AND b.CategoryId = @CategoryId");

            if (period.HasValue)
                sql.Append(" AND b.BudgetPeriodId = @Period");

            sql.Append(@"
            ORDER BY b.Id
            OFFSET @Offset ROWS
            FETCH NEXT @PageSize ROWS ONLY;
            ");

            var parameters = new
            {
                UserId = userId,
                Name = name != null ? $"%{name}%" : null,
                CategoryId = categoryId,
                Period = period,
                Offset = (page - 1) * pageSize,
                PageSize = pageSize
            };

            var command = new CommandDefinition(
                sql.ToString(),
                parameters,
                cancellationToken: token
            );

            return await connection.QueryAsync<BudgetInfoDto>(command);
        }
        // Get by Id + UserId
        public async Task<Budget?> GetByIdAndUserIdAsync(Guid userId, Guid id, CancellationToken token)
        {
            using var connection = _connectionFactory.GetConnection();

            const string sql = @"
                SELECT *
                FROM Budgets
                WHERE Id = @Id AND UserId = @UserId";

            var cmd = new CommandDefinition(sql, new { Id = id, UserId = userId }, cancellationToken: token);

            var model = await connection.QuerySingleOrDefaultAsync<BudgetModel>(cmd);

            return model is null ? null : BudgetMapper.MapToDomain(model);
        }

        // Get DTO by Id + UserId
        public async Task<BudgetDto?> GetDtoByIdAndUserIdAsync(Guid userId, Guid id, CancellationToken token)
        {
            var model = await GetByIdAndUserIdAsync(userId, id, token);
            return model is null ? null : MapToDto(BudgetMapper.MapToModel(model));
        }

        // Mapping helper
        private static BudgetDto MapToDto(BudgetModel model)
        {
            return new BudgetDto(
                model.Id,
                model.UserId,
                model.Name,
                model.CategoryId,
                model.Amount,
                (EnCurrency)model.CurrencyId,
                model.BudgetPeriodId,
                model.StartDate,
                model.EndDate
            );
        }
    }
}