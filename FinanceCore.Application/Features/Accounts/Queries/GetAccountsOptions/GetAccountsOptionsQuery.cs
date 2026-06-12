using FinanceCore.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Accounts.Queries.GetAccountByUserOptions
{
    public record GetAccountsOptionsQuery(Guid userId , int page = 1 , int pageSize = 10) : IRequest<IEnumerable<AccountOptionsDto>>;
}
