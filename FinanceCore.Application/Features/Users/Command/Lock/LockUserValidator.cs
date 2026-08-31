
using FluentValidation;

namespace FinanceCore.Application.Features.Users.Command.Lock
{
    public class LockUserValidator : AbstractValidator<LockUserCommand>
    {
        public LockUserValidator() { 
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.LockedUntil).GreaterThanOrEqualTo(DateTime.UtcNow).NotEmpty();
        }
    }
}
