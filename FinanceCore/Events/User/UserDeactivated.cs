using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.User
{
    public record UserDeactivatedEvent(
        Guid UserId,
        string Name) : DomainEvent;
}
