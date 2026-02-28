using Dapper;
using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Budgets;
using FinanceCore.Domain.Enums;
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
        public async Task<BudgetDto?> GetByCategoryIdAsync(Guid userId, Guid categoryId, DateTime start, DateTime end)
        {
            using var connection = _connectionFactory.GetConnection();

            // Query to get the budget that overlaps the period
            var sql = @"
            SELECT TOP 1 *
            FROM Budgets
            WHERE UserId = @UserId
            AND CategoryId = @CategoryId
            AND StartDate <= @End
            AND EndDate >= @Start
            ORDER BY StartDate DESC";  
            // QuerySingleOrDefault maps to BudgetModel
            var budgetModel = await connection.QuerySingleOrDefaultAsync<BudgetModel>(
                sql,
                new
                {
                    UserId = userId,
                    CategoryId = categoryId,
                    Start = start,
                    End = end
                }
            );

            if (budgetModel == null)
                return null;

            // Map BudgetModel to BudgetDto
            return new BudgetDto(
                budgetModel.Id,
                budgetModel.UserId,
                budgetModel.CategoryId,
                budgetModel.Amount,
                (EnCurrency)budgetModel.CurrencyId, // convert byte to enum
                budgetModel.Period,
                budgetModel.StartDate,
                budgetModel.EndDate
            );
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