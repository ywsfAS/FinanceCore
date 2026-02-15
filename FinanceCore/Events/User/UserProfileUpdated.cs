using FinanceCore.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Domain.Events.User
{
    public record UserProfileUpdatedEvent(
        Guid UserId,
        string Name) : DomainEvent;
}
