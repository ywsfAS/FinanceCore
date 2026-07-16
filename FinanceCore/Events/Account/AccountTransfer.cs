using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.Account
{
    public record AccountTransferEvent(
        Guid SourceAccountId,
        Guid TargetAccountId,
        Money Amount) : DomainEvent;
}