using FinanceCore.Domain.Enums;

namespace FinanceCore.API.Requests.ReccuringTransations
{
    public record CreateRecurringTransactionRequest(
        Guid AccountId,
        Guid CategoryId,
        decimal Amount,
        EnPeriod Period,
        EnExecutionType ExecutionType,
        string? Description,
        DateTime StartDate,
        DateTime? EndDate
        );
}
