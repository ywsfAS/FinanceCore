using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.Transaction
{
    public record TransactionUpdatedEvent(
        Guid TransactionId,
        Guid? AccountId) : DomainEvent;
}
