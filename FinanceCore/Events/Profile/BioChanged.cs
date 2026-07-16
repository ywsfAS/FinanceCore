using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.Profile
{
    public record ProfileBioUpdatedEvent(Guid userId , string newBio) : DomainEvent;
}
