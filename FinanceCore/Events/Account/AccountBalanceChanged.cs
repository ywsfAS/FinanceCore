using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Domain.Events.Account
{
    public record AccountBalanceChangedEvent(
        Guid AccountId,
        Money PreviousBalance,
        Money NewBalance,
        EnTransactionType TransactionType,
        Money Amount) : DomainEvent;
}
