using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.Profile
{
    public record ProfileNameUpdatedEvent(Guid userId , string firstName , string LastName ) : DomainEvent;
    
}
