using FinanceCore.Domain.Budgets;
using FinanceCore.Domain.Enums;
using FinanceCore.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Infrastructure.Mappers.ToDomain
{
    public static class BudgetMapper
    {
        public static Budget MapToDomain(BudgetModel model)
        {
            return Budget.Create(model.Id, model.UserId,model.CategoryId,model.Name,MoneyMapper.MapToDomain(model.Amount),MoneyMapper.MapToDomain(model.SpentMoney),(BudgetPeriod)model.Period,model.StartDate , model.EndDate);

            
        }

    }
}
