using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.Transaction
{
    public record TransactionAmountChangedEvent(
        Guid TransactionId,
        Guid? AccountId,
        Money OldAmount,
        Money NewAmount) : DomainEvent;
}
