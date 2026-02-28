using FinanceCore.Application.DTOs.Transaction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Transactions.Commands.Expense
{
    public record ExpenseCommand(Guid AccountId , Guid CategoryId , decimal Amount , string? Description , DateTime TransactionDate) : IRequest<ExpenseDto>;
}
