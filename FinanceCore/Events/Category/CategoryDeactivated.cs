using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.Category
{
    // Category deactivated
    public record CategoryDeactivatedEvent(
        Guid CategoryId,
        string Name) : DomainEvent;
}
