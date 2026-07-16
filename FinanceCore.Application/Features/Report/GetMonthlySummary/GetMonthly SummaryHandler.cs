using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using MediatR;

namespace FinanceCore.Application.Features.Report.GetMonthlySummary
{
    public class GetMonthlySummaryHandler : IRequestHandler<GetAccountsMonthlySummaryQuery,IEnumerable<MonthlySummaryDto>>
    {
        private readonly ITransactionRepository _transactionRepository;
        public GetMonthlySummaryHandler(ITransactionRepository transactionRepository) { 
            _transactionRepository = transactionRepository;
        }
        public async Task<IEnumerable<MonthlySummaryDto>> Handle(GetAccountsMonthlySummaryQuery query , CancellationToken token)
        {
            var startDate = new DateTime(query.Year,query.Month,1);
            var endDate = startDate.AddMonths(1);
            var result = await _transactionRepository.GetMonthlySummaryAsync(query.UserId,query.AccountId,startDate,endDate,query.Page,query.PageSize,token);
            return result;
        }
    }
}
