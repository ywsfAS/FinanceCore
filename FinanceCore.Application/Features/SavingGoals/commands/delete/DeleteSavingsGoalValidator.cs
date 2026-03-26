using FinanceCore.Application.Features.Goals.Commands.Delete;
using FluentValidation;

namespace FinanceCore.Application.Features.SavingGoals.commands.delete
{
    public class DeleteSavingsGoalValidator : AbstractValidator<DeleteSavingsGoalCommand>
    {
        public DeleteSavingsGoalValidator() { }
    }
}
