using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Report.GetMonthlySummaryPerUser
{
    public class GetMonthlySummaryHandler : IRequestHandler<GetMonthlySummaryQueryUser, IEnumerable<MonthlySummaryDto>?>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;
        public GetMonthlySummaryHandler(IAccountRepository accountRepository , ITransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository; 
        }
        public async Task<IEnumerable<MonthlySummaryDto>?> Handle(GetMonthlySummaryQueryUser query , CancellationToken token)
        {
           var accounts =  await _accountRepository.GetByUserIdAsync(query.userId);
           DateTime start = new DateTime(query.year, query.month, 1);
           DateTime end = start.AddMonths(1);
           var tasks = accounts.Select(account => _transactionRepository.GetMonthlySummary(account.Id, start, end));
           var reports = await Task.WhenAll(tasks);
            var result = reports.Select(report => new MonthlySummaryDto(Guid.NewGuid(),query.year, query.month, report.TotalIncome, report.TotalExpenses, report.TotalIncome - report.TotalExpenses));
            return result;

        }
    }
}
