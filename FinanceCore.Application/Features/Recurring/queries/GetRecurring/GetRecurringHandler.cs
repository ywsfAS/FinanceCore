using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs.RecurringTransaction;
using FinanceCore.Domain.Events.RecurringTransaction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Recurring.queries.GetRecurring
{
    public  class GetRecurringHandler : IRequestHandler<GetRecurringQuery,IEnumerable<RecurringTransactionDto>>
    {
        private readonly IRecurringTransactionRepository _recurringRepository;
        public GetRecurringHandler(IRecurringTransactionRepository recurringTransactionRepository) { 
            _recurringRepository = recurringTransactionRepository;
        }
        public async Task<IEnumerable<RecurringTransactionDto>> Handle(GetRecurringQuery query , CancellationToken token)
        {
            return await _recurringRepository.GetRecurringTransactionsAsync(query.UserId, query.AccountId, query.CategoryId, query.IsActive, query.Period, query.Start, query.End, query.Page, query.PageSize, token);

        }
        
    }
}
