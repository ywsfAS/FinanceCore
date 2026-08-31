using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs.RecurringTransaction;
using MediatR;

namespace FinanceCore.Application.Features.Recurring.Queries.GetRecurring
{
    public  class GetRecurringHandler : IRequestHandler<GetRecurringQuery,IEnumerable<RecurringTransactionDto>>
    {
        private readonly IRecurringTransactionRepository _recurringRepository;
        public GetRecurringHandler(IRecurringTransactionRepository recurringTransactionRepository) { 
            _recurringRepository = recurringTransactionRepository;
        }
        public async Task<IEnumerable<RecurringTransactionDto>> Handle(GetRecurringQuery query , CancellationToken token)
        {
            return await _recurringRepository.GetRecurringTransactionsAsync(query.UserId, query.AccountId, query.CategoryId, query.Status, query.Period, query.Start, query.End, query.Page, query.PageSize, token);

        }
        
    }
}
