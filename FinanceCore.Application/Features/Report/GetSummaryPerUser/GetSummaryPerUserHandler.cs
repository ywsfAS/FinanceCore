using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.DTOs.Transaction;
using FinanceCore.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Report.GetSummaryPerUser
{
    public class GetSummaryPerUserHandler : IRequestHandler<GetSummaryPerUserQuery, MonthlyUserSummaryDto?>
    {
        private readonly ITransactionRepository _transactionRepository;
        public GetSummaryPerUserHandler(IAccountRepository accountRepository , ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository; 
        }
        public async Task<MonthlyUserSummaryDto?> Handle(GetSummaryPerUserQuery query , CancellationToken token)
        {
           var report = await _transactionRepository.GetSummaryByUser(query.userId,token);
            return new MonthlyUserSummaryDto(query.userId, report.TotalIncome, report.TotalExpense); 

        }
    }
}
