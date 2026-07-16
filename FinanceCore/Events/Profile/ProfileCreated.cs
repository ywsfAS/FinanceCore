using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.Profile
{
    public record ProfileCreatedEvent(Guid userId) : DomainEvent;
}
