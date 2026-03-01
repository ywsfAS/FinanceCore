using FinanceCore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.DTOs.Transaction
{
    public record CreateTransferDto(
       Guid DebitTransactionId,
       Guid CreditTransactionId,
       Guid FromAccountId,
       Guid? ToAccountId,
       decimal Amount,
       decimal SourceBalance,
       decimal DestinationBalance,
       DateTime TransferDate,
       EnTransactionType Type = EnTransactionType.Transfer  
    );
}
