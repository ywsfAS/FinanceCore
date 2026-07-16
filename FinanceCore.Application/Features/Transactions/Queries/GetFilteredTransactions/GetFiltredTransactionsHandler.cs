using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs.Transaction;
using MediatR;

namespace FinanceCore.Application.Features.Transactions.Queries.GetFiltredTransactions
{
    public class GetFiltredTransactionsHandler : IRequestHandler<GetFiltredTransactionsQuery , IEnumerable<TransactionDto>>
    {
        private readonly ITransactionRepository _transactionRepository;
        public GetFiltredTransactionsHandler(ITransactionRepository transactionRepository) { 
            _transactionRepository = transactionRepository;
        }
        public async Task<IEnumerable<TransactionDto>> Handle(GetFiltredTransactionsQuery query , CancellationToken token)
        {
            return await _transactionRepository.GetFilteredTransactionsAsync(query.UserId,query.AccountId,query.ToAccountId,query.CategoryId,query.Start,query.End,query.Type , query.Page , query.PageSize,token);
        }
    }
}
