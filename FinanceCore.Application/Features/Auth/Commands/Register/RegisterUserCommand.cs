using FinanceCore.Application.DTOs.Auth;
using MediatR;
namespace FinanceCore.Application.Features.Auth.Commands.Register
{
    public record RegisterUserCommand(string Name,string Email ,string Password)
    : IRequest<RegisterDto>;
}
