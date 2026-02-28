using FinanceCore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.DTOs.Transaction
{
    public record ExpenseDto(Guid Id , Guid? CategoryId , decimal Amount , string? Decription , EnTransactionType Type = EnTransactionType.Expense);
}
