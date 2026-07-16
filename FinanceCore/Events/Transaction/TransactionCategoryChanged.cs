using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.Transaction
{
    public record TransactionCategoryChangedEvent(
        Guid TransactionId,
        Guid? OldCategoryId,
        Guid? NewCategoryId) : DomainEvent;
}
