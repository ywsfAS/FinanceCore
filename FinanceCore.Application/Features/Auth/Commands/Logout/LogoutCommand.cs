using MediatR;

namespace FinanceCore.Application.Features.Auth.Commands.Logout
{
    public sealed record LogoutCommand(string refreshToken) : IRequest;
}
