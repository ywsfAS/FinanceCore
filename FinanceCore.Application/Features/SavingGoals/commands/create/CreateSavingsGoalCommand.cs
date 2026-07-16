using FinanceCore.Application.DTOs.Goal;
using FinanceCore.Domain.Common;
using MediatR;

namespace FinanceCore.Application.Features.Goals.Commands.Create
{
    public record CreateSavingsGoalCommand(
        Guid UserId,
        string Name,
        Money TargetAmount,
        DateTime? TargetDate = null,
        string? Description = null
    ) : IRequest<SavingsGoalDto>;
}
