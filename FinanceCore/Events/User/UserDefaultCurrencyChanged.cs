using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;

namespace FinanceCore.Domain.Events.User
{
    public record UserDefaultCurrencyChangedEvent
      (Guid Id,
        EnCurrency Old,
        EnCurrency New  
      ) : DomainEvent;
}
