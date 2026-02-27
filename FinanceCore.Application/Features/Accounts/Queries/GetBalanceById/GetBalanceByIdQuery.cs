using FinanceCore.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Accounts.Queries.GetBalanceById
{
    public record GetBalanceByIdQuery(Guid AccountId) : IRequest<AccountBalanceDto>;
    
    
}
