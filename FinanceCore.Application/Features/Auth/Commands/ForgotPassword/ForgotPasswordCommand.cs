using MediatR;

namespace FinanceCore.Application.Features.Auth.Commands.ForgotPassword
{
    public sealed record ForgotPasswordCommand(string Email) : IRequest;
    
    
}
