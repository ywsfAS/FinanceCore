using FinanceCore.Domain.Budgets;
using FinanceCore.Domain.Enums;
using FinanceCore.Infrastructure.Models;
using FinanceCore.Domain.Common;
namespace FinanceCore.Infrastructure.Mappers
{
    public static class BudgetMapper
    {
        public static BudgetModel MapToModel(Budget budget)
        {
            return new BudgetModel { Id = budget.Id, UserId = budget.UserId, CategoryId = budget.CategoryId, Amount = budget.Amount.Amount, Period = budget.Period, StartDate = budget.StartDate , EndDate = budget.EndDate , CreatedAt = budget.CreatedAt, UpdatedAt = budget.UpdatedAt };


        }
        public static Budget MapToDomain(BudgetModel model)
        {
            return Budget.Create(model.Id, model.UserId, model.CategoryId, model.Name,(EnCurrency)model.CurrencyId, new Money(model.Amount),model.Period, model.StartDate, model.EndDate,model.CreatedAt,model.UpdatedAt);


        }
    }
}
