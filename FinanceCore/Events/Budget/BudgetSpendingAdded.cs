using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.Budget
{
    // Spending added to budget (tracks each transaction)
    public record BudgetSpendingAddedEvent(
        Guid BudgetId,
        Guid CategoryId,
        Money Amount,
        Money TotalSpent,
        decimal PercentageUsed) : DomainEvent;
}
