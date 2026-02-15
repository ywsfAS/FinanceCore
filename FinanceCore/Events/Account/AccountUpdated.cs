using FinanceCore.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Domain.Events.Account
{
    public record AccountUpdatedEvent
    (Guid AccountId,
        string Name
    )   : DomainEvent;     
}
