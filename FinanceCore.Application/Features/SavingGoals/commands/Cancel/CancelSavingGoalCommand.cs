using MediatR;

namespace FinanceCore.Application.Features.SavingGoals.Commands.Cancel
{
    public record CancelSavingGoalCommand(Guid Id , Guid UserId) : IRequest;
}
