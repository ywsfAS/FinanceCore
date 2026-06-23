using FinanceCore.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Report.GetBudgetHealth
{
    public record BudgetHealthQuery(Guid UserId , int Page = 1 , int PageSize = 10 ) : IRequest<BudgetHealthDto?>;
}
