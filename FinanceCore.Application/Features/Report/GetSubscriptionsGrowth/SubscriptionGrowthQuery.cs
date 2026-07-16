using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Enums;
using MediatR;

namespace FinanceCore.Application.Features.Report.GetSubscriptionsGrowth
{
    public record SubscriptionGrowthQuery(Guid UserId,Guid? AccountId, EnTransactionType Type , DateTime Start , DateTime End )  :IRequest<SubscriptionGrowthDto?>;
}
