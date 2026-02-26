using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Budgets;
using FinanceCore.Infrastructure.context;
using FinanceCore.Infrastructure.Mappers;
using FinanceCore.Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

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
            var model =  await _connectionFactory.ReadSingleAsync<BudgetModel, Guid>(
                "sp_GetBudgetById",
                id);
            if (model == null) {
               return null;
            }
            return BudgetMapper.MapToDomain(model);
        }

        public async Task<IEnumerable<Budget>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {

             var models = await _connectionFactory.ReadListAsync<BudgetModel>(
                "sp_GetBudgetsByUserId",
                new { UserId = userId });
            return models.Select(model => BudgetMapper.MapToDomain(model));
        }

        public async Task AddAsync(Budget budget, CancellationToken cancellationToken = default)
        {
            var model = BudgetMapper.MapToModel(budget);
            await _connectionFactory.ExecuteNonQueryAsync(
                "sp_CreateBudget",
                model
                );
        }

        public async Task UpdateAsync(Budget budget, CancellationToken cancellationToken = default)
        {
            var model = BudgetMapper.MapToModel(budget);
            await _connectionFactory.ExecuteNonQueryAsync(
                "sp_UpdateBudget",
                model
             );
        }

        public async Task DeleteAsync(Budget budget, CancellationToken cancellationToken = default)
        {
            await _connectionFactory.ExecuteNonQueryAsync(
                "sp_DeleteBudget",
                new { budget.Id });
        }
    }
}