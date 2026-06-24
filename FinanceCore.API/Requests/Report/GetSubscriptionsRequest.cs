using FinanceCore.Domain.Enums;

namespace FinanceCore.API.Requests.Report
{
    public record GetSubscriptionsRequest(Guid? AccountId , Guid? CategoryId , string? Name , EnPeriod? Period , EnTransactionType? Type);
}
