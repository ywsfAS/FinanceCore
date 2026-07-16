using FinanceCore.Domain.Enums;

namespace FinanceCore.Application.DTOs
{
    public record MonthlySummaryDto(Guid AccountId , decimal TotalIncome , decimal TotalExpense , decimal NetSavings, EnCurrency Currency);
    
    
}
