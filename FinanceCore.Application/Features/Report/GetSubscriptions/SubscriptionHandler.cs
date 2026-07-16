using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.RecurringTransaction;
using MediatR;
namespace FinanceCore.Application.Features.Report.GetSubscriptions
{
    public class SubscriptionHandler : IRequestHandler<SubscriptionQuery,SubscriptionDto>
    {
        private readonly IRecurringTransactionRepository _recurringTransactionRepository;
        private readonly ICurrencyConverter _CurrencyConverter;
        public SubscriptionHandler(IRecurringTransactionRepository recurringTransaction , ICurrencyConverter currencyConverter) { 
            _recurringTransactionRepository = recurringTransaction; 
            _CurrencyConverter = currencyConverter;
        }
        public async Task<SubscriptionDto> Handle(SubscriptionQuery query , CancellationToken token)
        {
            var subscriptions = await _recurringTransactionRepository.GetSubscriptionsAsync(query.UserId,query.AccountId,query.CategoryId,query.Name,query.Period,query.Type,query.Page , query.PageSize , token);
            var globalCurrency = EnCurrency.USD;
            foreach (var item in subscriptions) {
                var recurringTransaction = new RecurringTransaction
                {
                    StartDate = item.PerviousCharge
                };
                item.NextCharge = recurringTransaction.GetNextExecutionDate(DateTime.UtcNow);
            }
            var convertedTasks = subscriptions.Select(async (item) =>
            {
                return await _CurrencyConverter.Convert(item.Amount, item.Currency, globalCurrency, token);
            });
            var convertedResults = await Task.WhenAll(convertedTasks);
            decimal totalSubscription = convertedResults.Sum();
            return new SubscriptionDto(totalSubscription, subscriptions, new PaginationDto(subscriptions.Count(), query.Page, query.PageSize));

        }
    }
   
}
