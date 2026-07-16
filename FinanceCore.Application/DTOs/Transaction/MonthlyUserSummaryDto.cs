
namespace FinanceCore.Application.DTOs.Transaction
{
    public record MonthlyUserSummaryDto(Guid userId , decimal totalIncome , decimal totalExpense);
}
