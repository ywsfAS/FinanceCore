using MediatR;

namespace FinanceCore.Application.Features.Recurring.Commands.Cancel
{
    public sealed record CancelCommand(Guid UserId , Guid Id) : IRequest;
}
