using FluentValidation;

namespace FinanceCore.Application.Features.SavingGoals.Commands.Resume
{
    public class ResumeSavingGoalValidator : AbstractValidator<ResumeSavingGoalCommand>
    {
        public ResumeSavingGoalValidator() { 
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
        
        }
    }
}
