using FinanceCore.Domain.Enums;
using FluentValidation;

namespace FinanceCore.Application.Features.Accounts.Commands.Update
{

    public class UpdateAccountCommandValidator : AbstractValidator<UpdateAccountCommand>
    {
        public UpdateAccountCommandValidator()
        {
            RuleFor(x => x.AccountId)
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(x => x.AccountType).IsInEnum();
            When(x => x.AccountType == EnAccountType.Savings, () =>
            {
                RuleFor(x => x.AccrualFrequency).IsInEnum();
                RuleFor(x => x.CreditFrequency).IsInEnum();
                RuleFor(x => x.InterestRate).NotEmpty();

            });
        }
    }
}
