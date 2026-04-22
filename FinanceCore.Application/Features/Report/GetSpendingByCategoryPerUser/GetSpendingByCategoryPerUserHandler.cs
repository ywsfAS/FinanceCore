using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Report.GetSpendingByCategoryPerUser
{
    public class GetSpendingByCategoryPerUserHandler : IRequestHandler<GetSpendingByCategoryPerUserQuery,List<SpendingByCategoryDto>>
    {
        private readonly ITransactionRepository _transactionRepository;

        public GetSpendingByCategoryPerUserHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<List<SpendingByCategoryDto>> Handle(GetSpendingByCategoryPerUserQuery query , CancellationToken token)
        {
            DateTime start = new DateTime(query.year, query.month, 1);
            DateTime end = start.AddMonths(1);
            return await _transactionRepository.GetSpendingByCategoryForUser(query.userId, start, end);           

        }


    }
}
