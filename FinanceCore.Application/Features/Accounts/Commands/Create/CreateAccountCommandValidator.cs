using FinanceCore.Domain.Enums;
using FluentValidation;

namespace FinanceCore.Application.Features.Accounts.Commands.Create
{
    public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Type)
                .IsInEnum();
            When(x => x.Type == EnAccountType.Savings, () =>
            {
                RuleFor(x => x.InterestAccrualFrequency).IsInEnum();
                RuleFor(x => x.InterestCreditFrequency).IsInEnum();
                RuleFor(x => x.InterestRate).NotEmpty();

            });
            When(x => x.Type == EnAccountType.Credit, () =>
            {
                RuleFor(x => x.CreditLimit).NotEmpty();
                RuleFor(x => x.Fee).NotEmpty();
                RuleFor(x => x.FeePeriod).IsInEnum();   

            });
        }
    }


}
