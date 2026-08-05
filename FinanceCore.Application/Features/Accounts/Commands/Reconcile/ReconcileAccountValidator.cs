
using FluentValidation;

namespace FinanceCore.Application.Features.Accounts.Commands.Reconcile
{
    public class ReconcileAccountValidator : AbstractValidator<ReconcileAccountCommand>
    {
        public ReconcileAccountValidator() { 
            RuleFor(x => x.ActualBalance).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Reason).IsInEnum();
            RuleFor(x => x.Notes).MaximumLength(100);
            RuleFor(x => x.UserId).NotEmpty();


        }
    }
}
