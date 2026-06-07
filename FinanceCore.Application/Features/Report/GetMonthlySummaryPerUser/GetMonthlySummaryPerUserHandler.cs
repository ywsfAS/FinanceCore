using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.Features.Report.GetMonthlySummary;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Report.GetMonthlySummaryPerUser
{
    public class GetMonthlySummaryPerUserHandler : IRequestHandler<GetMonthlySummaryPerUserQuery,MonthlySummaryPerUserDto>
    {
        private readonly ITransactionRepository _transactionRepository;
        public GetMonthlySummaryPerUserHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task<MonthlySummaryPerUserDto?> Handle(GetMonthlySummaryPerUserQuery query, CancellationToken token)
        {
            var startDate = new DateTime(query.year, query.month, 1);
            var endDate =startDate.AddMonths(1);
            var model = await _transactionRepository.GetMonthlySumaryByUser(query.userId, startDate, endDate, token);
            if (model == null) { return null; }
            ;
            return new MonthlySummaryPerUserDto(model.TotalIncome,model.TotalExpense);


        }
    }
}
