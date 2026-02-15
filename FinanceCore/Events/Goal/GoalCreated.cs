using FinanceCore.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Domain.Events.Goal
{
    // Goal created
    public record GoalCreatedEvent(
        Guid GoalId,
        Guid UserId,
        string Name,
        Money TargetAmount) : DomainEvent;
}
