using FinanceCore.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Domain.Events.Goal
{
    // Goal completed
    public record GoalCompletedEvent(
        Guid GoalId,
        string Name,
        Money TargetAmount,
        Money FinalAmount) : DomainEvent;
}
