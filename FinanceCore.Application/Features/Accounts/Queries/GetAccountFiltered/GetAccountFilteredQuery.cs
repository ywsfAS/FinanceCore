using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Accounts.Queries.GetAccountFiltered
{
    public record  GetAccountFilteredQuery(Guid userId , string? name , EnAccountType? type , EnCurrency? currency) : IRequest<IEnumerable<AccountInfoDto>?>;
}
