using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Budgets.Queries.GetBudgetsFiltered
{
    public record GetBudgetsFilteredQuery(Guid userId , string? name , Guid? categoryId , EnPeriod? period , int page = 1 , int pageSize = 10) : IRequest<IEnumerable<BudgetInfoDto>?>;
}
