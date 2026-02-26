using FinanceCore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.DTOs
{
    public record TransactionDto(
     Guid Id,
     Guid? AccountId,
     Guid? ToAccount,
     Guid? CategoryId,
     decimal Amount,
     EnTransactionType Type,
     DateTime Date,
     string? Description);
}
