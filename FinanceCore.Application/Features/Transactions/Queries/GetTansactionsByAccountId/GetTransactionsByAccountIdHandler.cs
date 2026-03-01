using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs.Transaction;
using MediatR;
using System;

namespace FinanceCore.Application.Features.Transactions.Queries.GetTansactionsByAccountId
{
    public class GetTransactionsByAccountIdHandler : IRequestHandler<GetTransactionsByAccountIdQuery, IEnumerable<TransactionDto>?>
    {
        private readonly ITransactionRepository _transactionRepository;
        public GetTransactionsByAccountIdHandler(ITransactionRepository transactionRepository) { 
            _transactionRepository = transactionRepository;
        }
        public async Task<IEnumerable<TransactionDto>?> Handle(GetTransactionsByAccountIdQuery query , CancellationToken token
            )
        {
           return await _transactionRepository.FetchTransactionsByIdPageAsync(query.Id , query.Page , query.PageSize);
        }
    }
}
