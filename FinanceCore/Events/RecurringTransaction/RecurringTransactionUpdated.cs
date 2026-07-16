using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.RecurringTransaction
{
    public record RecurringTransactionUpdatedEvent(Guid id) : DomainEvent;
    
    
}
