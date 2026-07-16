using FinanceCore.Domain.Common;


namespace FinanceCore.Domain.Events.Profile
{
    public record ProfileAvatarUpdatedEvent(Guid userId , string avatarUrl) : DomainEvent;
}
