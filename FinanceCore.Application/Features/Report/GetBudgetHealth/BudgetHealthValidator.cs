using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Report.GetBudgetHealth
{
    public class BudgetHealthValidator : AbstractValidator<BudgetHealthQuery>
    {
        public BudgetHealthValidator() {

            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Page).NotEmpty().GreaterThan(1);
            RuleFor(x => x.PageSize).NotEmpty().GreaterThan(1);
        
        }
    }
}
