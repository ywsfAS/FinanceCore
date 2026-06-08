using FinanceCore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.DTOs.Transaction
{
    public record TransactionDto(
     Guid Id,
     string AccountName,
     string? ToAccountName,
     string? CategoryName,
     decimal Amount,
     EnCurrency Currency,
     EnTransactionType Type,
     DateTime Date,
     string? Description);
}
