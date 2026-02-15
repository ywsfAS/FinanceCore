using FinanceCore.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Domain.Events.Goal
{
    // Contribution added to goal
    public record GoalContributionAddedEvent(
        Guid GoalId,
        Money ContributionAmount,
        Money NewTotalAmount,
        decimal PercentageComplete) : DomainEvent;
}
