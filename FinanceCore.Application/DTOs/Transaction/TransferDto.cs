using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.DTOs.Transaction
{
    public record TransferDto(
       Guid DebitTransactionId,
       Guid CreditTransactionId,
       decimal SourceBalance,
       decimal DestinationBalance,
       DateTime TransferDate
    );
}
