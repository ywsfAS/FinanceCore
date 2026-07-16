using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.Account
{
    public record AccountDeactivatedEvent(
        Guid AccountId,
        string Name) : DomainEvent;
}
