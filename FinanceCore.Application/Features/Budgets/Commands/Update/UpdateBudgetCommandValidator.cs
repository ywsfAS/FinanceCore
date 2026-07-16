using FluentValidation;

namespace FinanceCore.Application.Features.Budgets.Commands.Update
{
    public class UpdateBudgetCommandValidator : AbstractValidator<UpdateBudgetCommand>
    {
        public UpdateBudgetCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();


            RuleFor(x => x.Period)
                .IsInEnum();

        }
    }

}
