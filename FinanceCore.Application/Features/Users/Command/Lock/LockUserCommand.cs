using MediatR;

namespace FinanceCore.Application.Features.Users.Command.Lock
{
    public record LockUserCommand(Guid UserId,DateTime LockedUntil) : IRequest;
}
