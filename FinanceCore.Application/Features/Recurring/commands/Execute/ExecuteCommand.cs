using MediatR;
namespace FinanceCore.Application.Features.Recurring.Commands.Execute
{
    public sealed record ExecuteCommand(Guid UserId , Guid Id) : IRequest;
}
