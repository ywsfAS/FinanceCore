using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;

namespace FinanceCore.Domain.Events.Account
{
    public record AccountCreated(
        Guid AccountId,
        string Name,
        EnAccountType Type,
        Money InitialBalance) : DomainEvent;
}
