using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.RecurringTransaction
{
    public record RecurringTransactionScheduleChanged(Guid id) : DomainEvent;
}
