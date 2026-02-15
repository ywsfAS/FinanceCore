using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;


namespace FinanceCore.Domain.Events.Category
{
    // Category created
    public record CategoryCreatedEvent(
        Guid CategoryId,
        Guid UserId,
        string Name,
        CategoryType Type) : DomainEvent;
}
