using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Enums;
using MediatR;

namespace FinanceCore.Application.Features.Report.GetSubscriptions
{
    public record SubscriptionQuery(Guid UserId,Guid? CategoryId , Guid? AccountId ,string? Name , EnPeriod? Period , EnTransactionType? Type , int Page = 1 , int PageSize = 10 ) : IRequest<SubscriptionDto>;
}
