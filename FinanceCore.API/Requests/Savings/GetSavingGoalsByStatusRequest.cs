using FinanceCore.Domain.Enums;

namespace FinanceCore.API.Requests.Savings
{
    public record GetSavingGoalsByStatusRequest(Guid UserId , EnGoalStatus Status , int Page = 1 , int PageSize = 5);
}
