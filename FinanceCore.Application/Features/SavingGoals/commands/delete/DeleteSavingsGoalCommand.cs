using MediatR;

namespace FinanceCore.Application.Features.Goals.Commands.Delete
{
    public record DeleteSavingsGoalCommand(Guid userId , Guid Id) : IRequest;
}

