using FluentValidation;

namespace FinanceCore.Application.Features.SavingGoals.Commands.Cancel
{
    public class CancelSavingGoalValidator : AbstractValidator<CancelSavingGoalCommand>
    {
        public CancelSavingGoalValidator() { 
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
