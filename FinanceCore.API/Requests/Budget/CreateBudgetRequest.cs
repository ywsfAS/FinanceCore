using FinanceCore.Domain.Enums;

namespace FinanceCore.API.Requests.Budget
{
    public record CreateBudgetRequest(
        Guid CategoryId,
        string name,
        decimal Amount,
        EnCurrency Currency,
        BudgetPeriod Period,
        DateTime StartDate);
}
