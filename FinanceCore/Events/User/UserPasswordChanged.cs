using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.User
{
    public record UserPasswordChangedEvent(
        Guid UserId) : DomainEvent;
}
