using MediatR;

namespace FinanceCore.Application.Features.SavingGoals.Commands.Pause
{
    public record PauseSavingGoalCommand(Guid Id,Guid UserId) : IRequest;
}
