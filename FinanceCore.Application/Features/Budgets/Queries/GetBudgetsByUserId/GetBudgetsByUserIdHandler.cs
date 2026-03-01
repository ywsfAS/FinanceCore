using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Budgets.Queries.GetBudgetsByUserId
{
    public class GetBudgetsByUserIdHandler : IRequestHandler<GetBudgetsByUserIdQuery,IEnumerable<BudgetDto>?>
    {
        private readonly IBudgetRepository _budgetRepository;
        public GetBudgetsByUserIdHandler(IBudgetRepository budgetRepository) { 
                _budgetRepository = budgetRepository;
        }
        public async Task<IEnumerable<BudgetDto>?> Handle(GetBudgetsByUserIdQuery query , CancellationToken token)
        {
            return await _budgetRepository.GetDtoByUserIdAsync(query.UserId);
        }
    }
}
