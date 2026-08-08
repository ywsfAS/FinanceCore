
using FluentValidation;

namespace FinanceCore.Application.Features.Recurring.Commands.Cancel
{
    public class CancelValidator : AbstractValidator<CancelCommand>
    {
        public CancelValidator() {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Id).NotEmpty();

        }
    }
}
