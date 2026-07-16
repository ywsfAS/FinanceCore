using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.Account
{
    public record AccountActivatedEvent(
        Guid AccountId,
        string Name) : DomainEvent;
}
