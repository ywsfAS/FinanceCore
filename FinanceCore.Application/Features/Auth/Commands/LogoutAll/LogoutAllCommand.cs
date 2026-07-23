using MediatR;

namespace FinanceCore.Application.Features.Auth.Commands.LogoutAll
{
    public sealed record LogoutAllCommand(Guid UserId) : IRequest;
}
