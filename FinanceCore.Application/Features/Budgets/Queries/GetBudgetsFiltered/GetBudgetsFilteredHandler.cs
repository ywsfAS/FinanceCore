using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Budgets.Queries.GetBudgetsFiltered
{
    public class GetBudgetsFilteredHandler : IRequestHandler<GetBudgetsFilteredQuery,IEnumerable<BudgetInfoDto>?>
    {
        private readonly IBudgetRepository _budgetRepository;
        public GetBudgetsFilteredHandler(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }
        public async Task<IEnumerable<BudgetInfoDto>?> Handle(GetBudgetsFilteredQuery query , CancellationToken token)
        {
            return await _budgetRepository.GetBudgetsFilteredAsync(query.userId, query.name, query.categoryId, query.period, query.page, query.pageSize, token);
        }

    }
}
