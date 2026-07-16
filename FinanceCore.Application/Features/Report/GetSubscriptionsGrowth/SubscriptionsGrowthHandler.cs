using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using MediatR;

namespace FinanceCore.Application.Features.Report.GetSubscriptionsGrowth
{
    public class SubscriptionsGrowthHandler : IRequestHandler<SubscriptionGrowthQuery,SubscriptionGrowthDto?>
    {
        private readonly IRecurringTransactionRepository _recurringTransactionRepository;
        public SubscriptionsGrowthHandler(IRecurringTransactionRepository recurringTransactionRepository) { 
        
            _recurringTransactionRepository = recurringTransactionRepository;
        
        }
        public async Task<SubscriptionGrowthDto?> Handle(SubscriptionGrowthQuery query , CancellationToken token)
        {
            var currentStart = query.Start;
            var currentEnd = query.End;
            var diff = currentStart - currentEnd;
            var previousStart = currentStart.Subtract(diff);
            var previousEnd = currentEnd.Subtract(diff);
            var subscriptions = await _recurringTransactionRepository.GetSubscriptionsGrowthAsync(query.UserId,query.AccountId,query.Type,currentStart,currentEnd,previousStart,previousEnd,token);
            if(subscriptions is null) return null;
            subscriptions.ChangePercent = ((subscriptions.CurrentPeriodTotal - subscriptions.PreviousPeriodTotal)/subscriptions.PreviousPeriodTotal) * 100;
            return subscriptions;
        }

    }
}
