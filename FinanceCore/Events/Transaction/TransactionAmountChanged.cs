using FinanceCore.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Domain.Events.Transaction
{
    public record TransactionAmountChangedEvent(
        Guid TransactionId,
        Guid AccountId,
        Money OldAmount,
        Money NewAmount) : DomainEvent;
}
