using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.User
{
    public record UserAccountUnlockedEvent(Guid Id,Email Email) : DomainEvent;

}
