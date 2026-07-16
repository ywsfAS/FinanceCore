using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.Category
{
    // Category updated
    public record CategoryUpdatedEvent(
        Guid CategoryId,
        string Name) : DomainEvent;
}
