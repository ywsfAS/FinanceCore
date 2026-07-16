using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.Category
{
    // Category activated
    public record CategoryActivatedEvent(
        Guid CategoryId,
        string Name) : DomainEvent;
}
