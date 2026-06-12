using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.SavingGoals.Queries.GetSavingGoalsFiltered
{
    public class GetSavingsGoalFilteredValidator : AbstractValidator<GetSavingsGoalFilteredQuery>
    {
        public GetSavingsGoalFilteredValidator() {
            RuleFor(x => x.userId).NotEmpty();
        }
    }
}
