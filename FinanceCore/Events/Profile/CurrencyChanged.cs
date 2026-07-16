using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;

namespace FinanceCore.Domain.Events.Profile
{
    public record ProfileCurrencyChangedEvent(Guid userId, EnCurrency currency) : DomainEvent;
}
