using FinanceCore.Application.DTOs.Goal;
using FinanceCore.Domain.Enums;
using MediatR;
using System;

namespace FinanceCore.Application.Features.Goals.Commands.Update
{
    public record UpdateSavingsGoalCommand(
        Guid UserId,
        Guid Id,
        string Name,
        decimal TargetAmount,
        DateTime? TargetDate = null,
        string? Description = null,
        EnGoalStatus Status = EnGoalStatus.Active
    ) : IRequest<SavingsGoalDto>;
}
