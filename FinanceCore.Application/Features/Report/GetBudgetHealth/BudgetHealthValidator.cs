using FluentValidation;

namespace FinanceCore.Application.Features.Report.GetBudgetHealth
{
    public class BudgetHealthValidator : AbstractValidator<BudgetHealthQuery>
    {
        public BudgetHealthValidator() {

            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Page).NotEmpty().GreaterThanOrEqualTo(1);
            RuleFor(x => x.PageSize).NotEmpty().GreaterThanOrEqualTo(1);
        
        }
    }
}
