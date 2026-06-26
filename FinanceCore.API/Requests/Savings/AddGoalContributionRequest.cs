using FinanceCore.Domain.Enums;

namespace FinanceCore.API.Requests.Savings
{
    public record AddGoalContributionRequest(Guid UserId ,Guid AccountId , decimal Amount , EnCurrency Currency , DateTime ContributionDate,string? Description = null);
}
