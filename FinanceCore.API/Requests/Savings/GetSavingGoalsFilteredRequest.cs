using FinanceCore.Domain.Enums;

namespace FinanceCore.API.Requests.Savings
{
    public record GetSavingGoalsFilteredRequest(Guid UserId ,string? Name, EnCurrency? Currency , EnGoalStatus? Status ,int Page = 1 , int PageSize = 5);
}
