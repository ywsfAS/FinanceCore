using FinanceCore.Domain.Enums;

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
