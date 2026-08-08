using MediatR;

namespace FinanceCore.Application.Features.Recurring.Commands.Resume
{
    public sealed record ResumeCommand(Guid UserId , Guid Id) : IRequest;
}
