using FluentValidation;

namespace FinanceCore.Application.Features.SavingGoals.Queries.GetSavingGoalById
{
    public class GetSavingGoalValidator : AbstractValidator<GetSavingGoalQuery>
    {
        public GetSavingGoalValidator() {
            RuleFor(x => x.id).NotEmpty();
            RuleFor(x => x.userId).NotEmpty();
        }
    }
}
