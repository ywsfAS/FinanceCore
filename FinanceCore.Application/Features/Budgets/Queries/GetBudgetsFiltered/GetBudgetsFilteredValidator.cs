using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Budgets.Queries.GetBudgetsFiltered
{
    public class GetBudgetsFilteredValidator : AbstractValidator<GetBudgetsFilteredQuery>
    {
        public GetBudgetsFilteredValidator() { 
            RuleFor(x => x.userId).NotEmpty();
        
        }
    }
}
