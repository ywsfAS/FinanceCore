using FluentValidation;

namespace FinanceCore.Application.Features.Recurring.Commands.Update
{
    public class UpdateRecurringValidator : AbstractValidator<UpdateRecurringCommand>
    {
        public UpdateRecurringValidator() {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.AccountId).NotEmpty();
            RuleFor(x => x.CategoryId).NotEmpty();

            RuleFor(x => x.Period).IsInEnum();
            RuleFor(x => x.Amount).NotEmpty().GreaterThan(0);

        }
    }
}
