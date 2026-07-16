using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs.RecurringTransaction;
using MediatR;

namespace FinanceCore.Application.Features.Recurring.Queries.GetRecurringById
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
