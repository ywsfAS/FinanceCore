using FinanceCore.Application.DTOs.Goal;
using MediatR;

namespace FinanceCore.API.Requests.Savings
{
    public record CreateSavingsGoalRequest(
    Guid id,
    string Name,
    decimal TargetAmount,
    DateTime? TargetDate = null,
    string? Description = null
);
}
