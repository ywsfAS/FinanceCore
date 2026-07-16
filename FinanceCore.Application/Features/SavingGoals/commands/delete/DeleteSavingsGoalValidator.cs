using FinanceCore.Application.Features.Goals.Commands.Delete;
using FluentValidation;

namespace FinanceCore.Application.Features.SavingGoals.Commands.Delete
{
    public class DeleteSavingsGoalValidator : AbstractValidator<DeleteSavingsGoalCommand>
    {
        public DeleteSavingsGoalValidator() {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.userId).NotEmpty();
        }
    }
}
