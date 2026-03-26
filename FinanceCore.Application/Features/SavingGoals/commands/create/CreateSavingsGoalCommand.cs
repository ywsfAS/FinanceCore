using FinanceCore.Application.DTOs.Goal;
using FinanceCore.Domain;
using FinanceCore.Domain.Enums;
using MediatR;
using System;

namespace FinanceCore.Application.Features.Goals.Commands.Create
{
    public record CreateSavingsGoalCommand(
        Guid UserId,
        string Name,
        decimal TargetAmount,
        DateTime? TargetDate = null,
        string? Description = null
    ) : IRequest<SavingsGoalDto>;
}
