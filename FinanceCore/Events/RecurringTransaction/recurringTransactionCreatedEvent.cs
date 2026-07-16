using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.RecurringTransaction
{
    public record recurringTransactionCreatedEvent(Guid id , Guid accountId , Money amount): DomainEvent;
}
