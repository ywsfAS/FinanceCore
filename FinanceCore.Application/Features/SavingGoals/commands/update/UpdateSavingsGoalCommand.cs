using FinanceCore.Application.DTOs.Goal;
using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using MediatR;
using System;

namespace FinanceCore.Application.Features.Goals.Commands.Update
{
    public record UpdateSavingsGoalCommand(
        Guid UserId,
        Guid Id,
        string Name,
        Money TargetAmount,
        DateTime? TargetDate = null,
        string? Description = null,
        EnGoalStatus Status = EnGoalStatus.Active
    ) : IRequest<SavingsGoalDto>;
}
