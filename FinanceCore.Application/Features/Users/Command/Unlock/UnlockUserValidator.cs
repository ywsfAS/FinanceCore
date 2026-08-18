
using FluentValidation;

namespace FinanceCore.Application.Features.Users.Command.Unlock
{
    public class UnlockUserValidator :AbstractValidator<UnlockUserCommand>
    {
        public UnlockUserValidator() { 
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
