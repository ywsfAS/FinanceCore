using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.SavingGoals.Queries.GetSavingGoalsByStatus
{
    public class GetSavingGoalByStatusValidator : AbstractValidator<GetSavingGoalByStatusQuery>
    {
        public GetSavingGoalByStatusValidator() {
            RuleFor(x => x.Status).IsInEnum();
            RuleFor(x => x.Page).NotEmpty().GreaterThanOrEqualTo(1);
            RuleFor(x => x.PageSize).NotEmpty().GreaterThanOrEqualTo(1);
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
