using FinanceCore.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Domain.Events.Goal
{
    // Withdrawal from goal
    public record GoalWithdrawalEvent(
        Guid GoalId,
        Money WithdrawalAmount,
        Money RemainingAmount) : DomainEvent;
}
