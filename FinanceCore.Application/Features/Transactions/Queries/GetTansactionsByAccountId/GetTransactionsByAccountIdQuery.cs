using FinanceCore.Application.DTOs.Transaction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Transactions.Queries.GetTansactionsByAccountId
{
    public record GetTransactionsByAccountIdQuery(Guid UserId,Guid Id, int Page , int PageSize ) : IRequest<IEnumerable<TransactionDto>?>;

}
