using FinanceCore.Application.DTOs.Auth;
using MediatR;

namespace FinanceCore.Application.Features.Auth.Commands.Refresh
{
    public sealed record RefreshCommand(string refreshToken) : IRequest<LoginDto>;
}
