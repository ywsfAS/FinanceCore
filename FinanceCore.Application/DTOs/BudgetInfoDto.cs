using FinanceCore.Domain.Enums;

namespace FinanceCore.Application.DTOs
{
    public record BudgetInfoDto
    (
        Guid Id,
        string Name,
        decimal Amount,
        EnCurrency Currency,
        EnPeriod Period,
        DateTime StartDate,
        DateTime EndDate,
        string CategoryName
    );
}
