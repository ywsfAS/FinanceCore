using FinanceCore.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Domain.Events.Goal
{
    // Goal target date updated
    public record GoalTargetDateUpdatedEvent(
        Guid GoalId,
        DateTime? OldTargetDate,
        DateTime? NewTargetDate) : DomainEvent;
}
