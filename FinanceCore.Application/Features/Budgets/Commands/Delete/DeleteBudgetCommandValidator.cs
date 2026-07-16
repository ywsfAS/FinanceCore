using FluentValidation;

namespace FinanceCore.Application.Features.Budgets.Commands.Delete
{
    public class DeleteBudgetCommandValidator : AbstractValidator<DeleteBudgetCommand>
    {
        public DeleteBudgetCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
