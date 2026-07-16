using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.RecurringTransaction
{
    public record desactivateRecurringTransactionEvent(Guid id) : DomainEvent;
}
