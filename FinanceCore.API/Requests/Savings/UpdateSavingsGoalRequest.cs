using FinanceCore.Domain.Enums;

namespace FinanceCore.API.Requests.Savings
{
    public record UpdateSavingsGoalRequest(
    string Name,
    decimal TargetAmount,
    EnCurrency Currency,
    DateTime? TargetDate = null,
    string? Description = null,
    EnGoalStatus Status = EnGoalStatus.Active
);
}
