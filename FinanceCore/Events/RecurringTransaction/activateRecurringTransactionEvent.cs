using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.RecurringTransaction
{
    public record activateRecurringTransactionEvent(Guid id) : DomainEvent;
}
