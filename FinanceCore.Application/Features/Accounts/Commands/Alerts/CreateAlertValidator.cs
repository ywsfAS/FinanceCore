
using FluentValidation;

namespace FinanceCore.Application.Features.Accounts.Commands.Alerts
{
    public class CreateAlertValidator : AbstractValidator<CreateLowBalanceAlertCommand>
    {
        public CreateAlertValidator() { 
            RuleFor(x => x.AccountId).NotEmpty();
            RuleFor(x => x.ThresholdAmount).GreaterThan(0);
        }
    }
}
