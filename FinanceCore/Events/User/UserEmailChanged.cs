using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.User
{
    public record UserEmailChangedEvent(
        Guid UserId,
        string OldEmail,
        string NewEmail) : DomainEvent;
}
