using FinanceCore.Domain.Enums;

namespace FinanceCore.Application.DTOs
{
    public record MonthlyTrendDto(string Month , decimal TotalIncome , decimal TotalExpense,decimal NetSavings , EnCurrency Currency);
}
