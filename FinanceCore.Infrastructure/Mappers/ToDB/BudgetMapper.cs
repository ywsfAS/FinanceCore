using FinanceCore.Domain.Budgets;
using FinanceCore.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Infrastructure.Mappers.ToDB
{
    public static class BudgetMapper
    {
        public static BudgetModel MapToModel(Budget budget)
        {
            return new BudgetModel { Id = budget.Id, UserId = budget.UserId, CategoryId = budget.CategoryId, Amount = MoneyMapper.MapToModel(budget.Amount), Period = (byte)budget.Period, StartDate = budget.StartDate , EndDate = budget.EndDate , CreatedAt = budget.CreatedAt, UpdatedAt = budget.UpdatedAt };


        }
    }
}
