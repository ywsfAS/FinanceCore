using FinanceCore.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Domain.Events.Goal
{
    // Goal resumed
    public record GoalResumedEvent(
        Guid GoalId,
        string Name) : DomainEvent;
}
