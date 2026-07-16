using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;

namespace FinanceCore.Domain.Events.Transaction
{
    public record TransactionVoidedEvent(
        Guid TransactionId,
        Guid? AccountId,
        Money Amount,
        EnTransactionType Type,
        string Reason,
        EnTransactionStatus PreviousStatus) : DomainEvent;
}
