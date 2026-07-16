using MediatR;

namespace FinanceCore.Application.Features.SavingGoals.Commands.Resume
{
    public record ResumeSavingGoalCommand(Guid Id , Guid UserId) : IRequest;
}
