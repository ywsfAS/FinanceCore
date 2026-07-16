using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.Account
{
    public record AccountBalanceAdjustedEvent(
        Guid AccountId,
        Money PreviousBalance,
        Money NewBalance,
        string Reason) : DomainEvent;
}
