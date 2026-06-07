using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.Features.Report.GetMonthlySummaryPerUser;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Report.GetMonthlyTrend
{
    public class MonthlyTrendHandler : IRequestHandler<MonthlyTrendQuery, IEnumerable<MonthlyTrendDto>?>
    {
        private readonly ITransactionRepository _transactionRepository;
        public MonthlyTrendHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task<IEnumerable<MonthlyTrendDto>?> Handle(MonthlyTrendQuery query, CancellationToken token)
        {
            var trend = await _transactionRepository.GetMonthlyTrend(query.userId,query.month);
            if (trend is null) return null;
            return trend;
        }
    }
}

