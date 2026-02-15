using FinanceCore.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Domain.Events.Goal
{
    // Goal milestone reached (e.g., 25%, 50%, 75%)
    public record GoalMilestoneReachedEvent(
        Guid GoalId,
        string GoalName,
        decimal PercentageComplete,
        Money CurrentAmount,
        Money TargetAmount) : DomainEvent;
}
