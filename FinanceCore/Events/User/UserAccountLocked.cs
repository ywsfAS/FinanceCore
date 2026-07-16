using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.User
{
    public record UserAccountLockedEvent(Guid id,DateTime dureation) : DomainEvent;

}
