using FluentValidation;

namespace FinanceCore.Application.Features.Budgets.Commands.Create
{

    public class CreateBudgetCommandValidator : AbstractValidator<CreateBudgetCommand>
    {
        public CreateBudgetCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();

            RuleFor(x => x.CategoryId)
                .NotEmpty();
            RuleFor(x => x.Period)
                .IsInEnum();

        }
    }
}
