using FinanceCore.Application.DTOs.Auth;
using MediatR;

namespace FinanceCore.Application.Features.Auth.Commands.Login
{
    public sealed record LoginUserCommand(string Email , string Password) : IRequest<LoginDto>;

}
