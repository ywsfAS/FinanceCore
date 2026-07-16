using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;

namespace FinanceCore.Domain.Events.Transaction
{
    public record TransactionCreatedEvent(
        Guid TransactionId,
        Guid AccountId,
        Guid? ToAccountId,
        Money Amount,
        EnTransactionType Type,
        DateTime Date) : DomainEvent;
}
