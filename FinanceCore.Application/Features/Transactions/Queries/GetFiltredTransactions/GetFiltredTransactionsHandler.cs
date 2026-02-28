using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Transactions.Queries.GetFiltredTransactions
{
    public class GetFiltredTransactionsHandler : IRequestHandler<GetFiltredTransactionsQuery , IEnumerable<TransactionDto?>>
    {
        private readonly ITransactionRepository _transactionRepository;
        public GetFiltredTransactionsHandler(ITransactionRepository transactionRepository) { 
            _transactionRepository = transactionRepository;
        }
        public async Task<IEnumerable<TransactionDto>?> Handle(GetFiltredTransactionsQuery query , CancellationToken token)
        {
            return await _transactionRepository.GetFiltredTransactionsAsync(query.CategoryId,query.Start,query.End,(byte?)query.Type , query.Page , query.PageSize);
        }
    }
}
