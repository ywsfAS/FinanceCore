using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using MediatR;

namespace FinanceCore.Application.Features.Transactions.Export
{
    public class ExportCSVHandler : IRequestHandler<ExportCSVQuery,ExportCSVDto>
    {
        private readonly ITransactionExporter _exporter;
        private readonly ITransactionRepository _repo;
        public ExportCSVHandler(ITransactionRepository repo , ITransactionExporter exporter) { 
            _exporter = exporter;
            _repo = repo;
        }
        public async Task<ExportCSVDto> Handle(ExportCSVQuery query , CancellationToken token)
        {
            var transactions = await _repo.GetFilteredTransactionsAsync(query.UserId,query.AccountId,query.ToAccountId,query.CategoryId,query.Start,query.End,query.Type,query.Page,query.PageSize,token);
            return _exporter.ExportCSV(transactions);


        }
    }
} 