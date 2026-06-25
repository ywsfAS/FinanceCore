using FinanceCore.Domain.Enums;

namespace FinanceCore.API.Requests.Report
{
    public record GetSubscriptionGrowthRequest( Guid? AccountId , EnTransactionType Type , DateTime Start , DateTime End);
}
