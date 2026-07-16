using MediatR;

namespace FinanceCore.Application.Features.Auth.Commands.ResetPassword
{
    public sealed record ResetPasswordCommand(string Token , string NewPassword) : IRequest;
}
