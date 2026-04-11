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
    public class GetMonthlySummaryHandler : IRequestHandler<GetMonthlySummaryQuery, MonthlySummaryDto?>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;
        public GetMonthlySummaryHandler(IAccountRepository accountRepository , ITransactionRepository transactionRepository) { 
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }
        public async Task<MonthlySummaryDto?> Handle(GetMonthlySummaryQuery query , CancellationToken token)
        {
            var account = await _accountRepository.GetDtoByIdAndUserIdAsync(query.UserId,query.Id);
            if (account == null) { return null; };   
            // Get Range of a month
            var StartDate = new DateTime(query.year,query.month,1);
            var EndDate = StartDate.AddDays(1);
            var result = await _transactionRepository.GetMonthlySummary(query.Id,StartDate,EndDate);
            if(result == null) { return null; };
            return new MonthlySummaryDto(query.Id,query.year,query.month,result.TotalIncome,result.TotalExpenses,result.TotalIncome - result.TotalExpenses);


        }
    }
}
