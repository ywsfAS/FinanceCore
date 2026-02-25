using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace FinanceCore.Domain.Events.Transaction
{
    public record TransactionVoidedEvent(
        Guid TransactionId,
        Guid? AccountId,
        Money Amount,
        EnTransactionType Type,
        string Reason,
        EnTransactionStatus PreviousStatus) : DomainEvent;
}
