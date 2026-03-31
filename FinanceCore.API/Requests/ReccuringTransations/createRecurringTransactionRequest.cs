using FinanceCore.Domain.Enums;

namespace FinanceCore.API.Requests.ReccuringTransations
{
    public record CreateRecurringTransactionRequest(
        Guid accountId,
        Guid categoryId,
        decimal amount,
        EnTransactionType type,
        EnPeriod period,
        int interval,
        bool isActive,
        string? description,
        DateTime startDate,
        DateTime? endDate
        );
}
