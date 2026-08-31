
using FluentValidation;

namespace FinanceCore.Application.Features.Recurring.Commands.Pause
{
    public class PauseValidator : AbstractValidator<PauseCommand>
    {
        public PauseValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
