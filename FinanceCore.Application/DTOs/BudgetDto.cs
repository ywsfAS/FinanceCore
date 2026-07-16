using FinanceCore.Domain.Enums;

namespace FinanceCore.Application.DTOs
{
    public record BudgetDto(
        Guid Id,
        Guid UserId,
        string Name,
        Guid CategoryId,
        decimal Amount,
        EnCurrency Currency,
        EnPeriod Period,
        DateTime StartDate,
        DateTime EndDate);
}
