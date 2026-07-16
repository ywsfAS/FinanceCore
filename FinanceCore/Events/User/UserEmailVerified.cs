using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.User
{
    public record UserEmailVerifiedEvent(Guid id, string email) : DomainEvent;

}
