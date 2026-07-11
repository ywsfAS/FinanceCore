using FinanceCore.Application.DTOs;
using FinanceCore.Application.DTOs.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Abstractions
{
    public interface ITransactionExporter
    {
        ExportCSVDto ExportCSV(IEnumerable<TransactionDto> transactions);

    }
}
