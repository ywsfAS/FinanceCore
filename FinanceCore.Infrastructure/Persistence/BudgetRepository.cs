using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Budgets;
using FinanceCore.Infrastructure.context;

namespace FinanceCore.Infrastructure.Repositories
{
    public class BudgetRepository : IBudgetRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public BudgetRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Budget?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _connectionFactory.ReadSingleAsync<Budget, Guid>(
                "sp_GetBudgetById",
                id);
        }

        public async Task<IEnumerable<Budget>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _connectionFactory.ReadListAsync<Budget>(
                "sp_GetBudgetsByUserId",
                new { UserId = userId });
        }

        public async Task AddAsync(Budget budget, CancellationToken cancellationToken = default)
        {
            await _connectionFactory.ExecuteNonQueryAsync(
                "sp_CreateBudget",
                new
                {
                    budget.Id,
                    budget.UserId,
                    budget.CategoryId,
                    Amount = budget.Amount.Amount,
                    Currency = budget.Amount.Currency.ToString(),
                    Period = budget.Period.ToString(),
                    budget.StartDate,
                    budget.EndDate,
                    budget.CreatedAt
                });
        }

        public async Task UpdateAsync(Budget budget, CancellationToken cancellationToken = default)
        {
            await _connectionFactory.ExecuteNonQueryAsync(
                "sp_UpdateBudget",
                new
                {
                    budget.Id,
                    Amount = budget.Amount.Amount,
                    Currency = budget.Amount.Currency.ToString(),
                    Period = budget.Period.ToString(),
                    budget.StartDate,
                    budget.EndDate,
                    budget.UpdatedAt
                });
        }

        public async Task DeleteAsync(Budget budget, CancellationToken cancellationToken = default)
        {
            await _connectionFactory.ExecuteNonQueryAsync(
                "sp_DeleteBudget",
                new { budget.Id });
        }
    }
}