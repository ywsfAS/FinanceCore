using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Transactions.Queries.GetFiltredTransactions
{
    public record GetFiltredTransactionsQuery(Guid? CategoryId , DateTime? Start , DateTime? End , EnTransactionType? Type , int Page , int PageSize) : IRequest<IEnumerable<TransactionDto>?>;
}
