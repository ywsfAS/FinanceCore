using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.User
{
    public record UserActivatedEvent(
        Guid UserId,
        string Name) : DomainEvent;
}
