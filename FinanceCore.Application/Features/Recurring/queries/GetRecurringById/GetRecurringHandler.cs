using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs.RecurringTransaction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Recurring.queries.GetRecurringById
{
    public class GetRecurringHandler : IRequestHandler<GetRecurringByIdQuery,RecurringTransactionDto?>
    {
        private readonly IRecurringTransactionRepository _recurringRepository;
        public GetRecurringHandler(IRecurringTransactionRepository recurringTransactionRepository) { 
            _recurringRepository = recurringTransactionRepository;
        }
       public async Task<RecurringTransactionDto?> Handle(GetRecurringByIdQuery query , CancellationToken token)
        {
            return await _recurringRepository.GetDtoByIdAsync(query.UserId,query.Id);
        } 
    }
}
