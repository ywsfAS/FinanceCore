using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.Features.Report.GetMonthlySummary;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Report.GetMonthlySummaryPerAccount
{
    public class GetMonthlySummaryHandler : IRequestHandler<GetMonthlySummaryQuery, MonthlyAccountSummaryDto?>
    {
        private readonly ITransactionRepository _transactionRepository;
        public GetMonthlySummaryHandler(ITransactionRepository transactionRepository) { 
            _transactionRepository = transactionRepository;
        }
        public async Task<MonthlyAccountSummaryDto?> Handle(GetMonthlySummaryQuery query , CancellationToken token)
        {
            var StartDate = new DateTime(query.year,query.month,1);
            var EndDate = StartDate.AddMonths(1);
            var result = await _transactionRepository.GetMonthlySummary(query.Id,StartDate,EndDate);
            if(result == null) { return null; };
            return new MonthlyAccountSummaryDto(query.Id,query.year,query.month,result.TotalIncome,result.TotalExpense,result.TotalIncome - result.TotalExpense);


        }
    }
}
