using FinanceCore.Domain.Enums;

namespace FinanceCore.API.Requests.ReccuringTransations
{
    public record UpdateRecurringTransactionRequest(
        Guid Id,
        Guid AccountId,
        Guid CategoryId,
        decimal Amount,
        EnTransactionType Type,
        EnPeriod Period,
        int Interval,
        string? Description,
        DateTime StartDate,
        DateTime? EndDate,
        bool IsActive
        );
}
