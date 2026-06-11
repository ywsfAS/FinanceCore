using FinanceCore.Domain.Enums;

namespace FinanceCore.API.Requests.Budget
{
    public record GetBudgetsFilteredRequest
    (
    string? Name,
    Guid? CategoryId,
    EnPeriod? Period,
    int Page = 1,
    int PageSize = 10
    );
}
