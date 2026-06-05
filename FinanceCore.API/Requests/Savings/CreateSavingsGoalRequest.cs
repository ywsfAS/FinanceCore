using FinanceCore.Application.DTOs.Goal;
using FinanceCore.Domain.Enums;
using MediatR;

namespace FinanceCore.API.Requests.Savings
{
    public record CreateSavingsGoalRequest(
    string Name,
    decimal TargetAmount,
    EnCurrency Currency,
    DateTime? TargetDate = null,
    string? Description = null
);
}
