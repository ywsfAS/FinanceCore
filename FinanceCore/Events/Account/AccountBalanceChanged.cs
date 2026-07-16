using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;

namespace FinanceCore.Domain.Events.Account
{
    public record AccountBalanceChangedEvent(
        Guid AccountId,
        Money PreviousBalance,
        Money NewBalance,
        EnTransactionType TransactionType,
        Money Amount) : DomainEvent;
}
