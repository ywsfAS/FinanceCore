using FluentValidation;

namespace FinanceCore.Application.Features.Recurring.Commands.Execute
{
    public class ExecuteValidator : AbstractValidator<ExecuteCommand>
    {
        public ExecuteValidator() {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
