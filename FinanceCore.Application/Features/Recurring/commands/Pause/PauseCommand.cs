using MediatR;

namespace FinanceCore.Application.Features.Recurring.Commands.Pause
{
    public sealed record PauseCommand(Guid UserId,Guid Id) : IRequest;
}
