using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Accounts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Accounts.Queries.GetAccountsByUserId
{
    public record GetAccountsByUserIdQuery(Guid UserId) : IRequest<IEnumerable<AccountDto>?>;
}
