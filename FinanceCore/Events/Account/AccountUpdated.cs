using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.Account
{
    public record AccountUpdatedEvent
    (Guid AccountId,
        string Name
    )   : DomainEvent;     
}
