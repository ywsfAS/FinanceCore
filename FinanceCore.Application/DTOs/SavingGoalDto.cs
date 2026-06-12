using FinanceCore.Domain.Enums;
using System;

namespace FinanceCore.Application.DTOs.Goal
{
    public record SavingsGoalDto(
        Guid Id,
        Guid UserId,
        string Name,
        string? Description,
        decimal TargetAmount,
        decimal CurrentAmount,
        EnCurrency Currency,
        DateTime? TargetDate,
        EnGoalStatus Status,
        DateTime? CompletedAt
    );
}
