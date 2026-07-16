
namespace FinanceCore.Application.DTOs
{
    public record BudgetProgressDto(decimal BudgetAmount , decimal SpentAmount , decimal Remaining , decimal PercentageUsed , bool IsExceeded);
}
