using FinanceCore.Application.Features.Goals.Commands.Create;
using FluentValidation;

namespace FinanceCore.Application.Features.SavingGoals.Commands.Create
{
    public class CreateSavingsGoalValidator : AbstractValidator<CreateSavingsGoalCommand>
    {
        public CreateSavingsGoalValidator() {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
        
        }
    }
}
