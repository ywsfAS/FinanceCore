using FinanceCore.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Accounts.Queries.GetAccountByName
{
    public record  GetAccountByNameQuery(Guid userId , string name) : IRequest<IEnumerable<AccountDto>?>;
}
