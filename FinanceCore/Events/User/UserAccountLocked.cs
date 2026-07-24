using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.User
{
    public record UserAccountLockedEvent(Guid Id,DateTime LockedUntil,Email Email) : DomainEvent;

}
