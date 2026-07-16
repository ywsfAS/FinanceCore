using FinanceCore.Application.DTOs;
using FinanceCore.Application.DTOs.Transaction;

namespace FinanceCore.Application.Abstractions
{
    public interface ITransactionExporter
    {
        ExportCSVDto ExportCSV(IEnumerable<TransactionDto> transactions);

    }
}
