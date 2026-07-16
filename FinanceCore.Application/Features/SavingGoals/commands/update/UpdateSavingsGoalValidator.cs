using FinanceCore.Application.Features.Goals.Commands.Update;
using FluentValidation;

namespace FinanceCore.Application.Features.SavingGoals.Commands.Update
{
    public class UpdateSavingsGoalValidator : AbstractValidator<UpdateSavingsGoalCommand>
    {
        public UpdateSavingsGoalValidator() { 
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Status).IsInEnum();
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.TargetDate).GreaterThan(DateTime.UtcNow);
        }
    }
}
