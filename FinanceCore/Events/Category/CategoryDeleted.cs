using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.Category
{
    // Category deleted (if you add soft delete)
    public record CategoryDeletedEvent(
        Guid CategoryId,
        Guid UserId,
        string Name) : DomainEvent;
}
