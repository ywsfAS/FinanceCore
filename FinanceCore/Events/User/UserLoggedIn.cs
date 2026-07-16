using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.User
{
    public record UserLoggedInEvent(
        Guid UserId,
        DateTime LoginTime) : DomainEvent;
}
