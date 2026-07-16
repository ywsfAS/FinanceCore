using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.User
{
    public record UserProfileUpdatedEvent(
        Guid UserId,
        string Name) : DomainEvent;
}
