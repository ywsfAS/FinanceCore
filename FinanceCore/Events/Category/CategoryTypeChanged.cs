using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;

namespace FinanceCore.Domain.Events.Category
{
       public record CategoryTypeChangedEvent(Guid id, CategoryType Old , CategoryType New) : DomainEvent;
}
