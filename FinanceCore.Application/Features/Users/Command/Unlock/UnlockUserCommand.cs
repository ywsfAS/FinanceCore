
using MediatR;

namespace FinanceCore.Application.Features.Users.Command.Unlock
{
    public record UnlockUserCommand(Guid UserId) : IRequest;
}
