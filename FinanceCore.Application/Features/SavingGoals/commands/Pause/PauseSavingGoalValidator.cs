using FluentValidation;

namespace FinanceCore.Application.Features.SavingGoals.Commands.Pause
{
    public class PauseSavingGoalValidator : AbstractValidator<PauseSavingGoalCommand>
    {
        public PauseSavingGoalValidator() { 
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
