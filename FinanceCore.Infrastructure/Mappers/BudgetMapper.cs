using FinanceCore.Application.Models;
using FinanceCore.Domain.Budgets;
using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
namespace FinanceCore.Infrastructure.Mappers
{
    public static class BudgetMapper
    {
        public static BudgetModel MapToModel(Budget budget)
        {
            return new BudgetModel { Id = budget.Id, UserId = budget.UserId,Name = budget.Name ,CurrencyId = (byte)budget.Amount.Currency,CategoryId = budget.CategoryId, Amount = budget.Amount.Amount, BudgetPeriodId = budget.Period, StartDate = budget.StartDate , EndDate = budget.EndDate , CreatedAt = budget.CreatedAt, UpdatedAt = budget.UpdatedAt , RowVersion = budget.RowVersion};


        }
        public static Budget MapToDomain(BudgetModel model)
        {
            return Budget.Load(model.Id, model.UserId, model.CategoryId, model.Name, new Money(model.Amount, (EnCurrency)model.CurrencyId),model.BudgetPeriodId, model.StartDate, model.EndDate,model.CreatedAt,model.UpdatedAt ,new Money(model.Amount,(EnCurrency)model.CurrencyId),model.RowVersion);


        }
    }
}
