using FluentValidation;

namespace FinanceCore.Application.Features.SavingGoals.Queries.GetSavingGoalsFiltered
{
    public class GetSavingsGoalFilteredValidator : AbstractValidator<GetSavingsGoalFilteredQuery>
    {
        public GetSavingsGoalFilteredValidator() {
            RuleFor(x => x.userId).NotEmpty();
            RuleFor(x => x.Page).GreaterThan(0).NotEmpty();
            RuleFor(x => x.PageSize).GreaterThan(0).NotEmpty();
        }
    }
}
