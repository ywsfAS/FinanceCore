using FinanceCore.Domain.Enums;

namespace FinanceCore.API.Requests.Savings
{
    public record UpdateSavingsGoalRequest(
    Guid Id,
    string Name,
    decimal TargetAmount,
    DateTime? TargetDate = null,
    string? Description = null,
    EnGoalStatus Status = EnGoalStatus.Active
);
}
