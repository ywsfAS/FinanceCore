
using FluentValidation;

namespace FinanceCore.Application.Features.Auth.Commands.LogoutAll
{
    public sealed class LogoutAllValidator : AbstractValidator<LogoutAllCommand>
    {
        public LogoutAllValidator() { 
            RuleFor(x => x.UserId).NotEmpty(); 
        }
    }
}
