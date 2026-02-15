using FinanceCore.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Domain.Events.Goal
{
    // Goal cancelled
    public record GoalCancelledEvent(
        Guid GoalId,
        Guid UserId,
        string Name,
        Money CurrentAmount) : DomainEvent;
}
 