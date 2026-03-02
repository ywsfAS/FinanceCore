using FinanceCore.Application.DTOs.Transaction;
using FinanceCore.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Transactions.Commands.Transactions
{
    public record TransactionCommand(Guid UserId ,Guid AccountId , Guid CategoryId ,EnTransactionType Type,decimal Amount , string? Description , DateTime TransactionDate) : IRequest<CreateTransactionDto>;
}
