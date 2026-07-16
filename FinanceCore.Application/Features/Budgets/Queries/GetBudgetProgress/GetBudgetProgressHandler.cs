using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using MediatR;
using FinanceCore.Domain.Exceptions;

namespace FinanceCore.Application.Features.Budgets.Queries.GetBudgetProgress
{

    
    public class GetBudgetProgressHandler : IRequestHandler<GetBudgetProgressQuery , BudgetProgressDto>
    {
        private readonly IBudgetRepository _budgetRepository;
        private readonly ITransactionRepository _transactionRepository;
        public GetBudgetProgressHandler(IBudgetRepository budgetRepository , ITransactionRepository transactionRepository) { 
            _budgetRepository = budgetRepository;
            _transactionRepository = transactionRepository;
        
        }
        public async Task<BudgetProgressDto> Handle(GetBudgetProgressQuery query , CancellationToken token)
        {
            // Get Budget
            var budget = await _budgetRepository.GetByIdAndUserIdAsync(query.UserId,query.Id,token);
            if (budget == null) { 
                throw new BudgetNotFoundException(query.Id);
            }
            var spent = await _transactionRepository.GetTotalSpendingByCategoryAsync(
                budget.UserId,
                budget.CategoryId,
                budget.StartDate,
                budget.EndDate,
                token
            );
            var remaining = budget.Amount.Amount - spent;
            var percentageUsed = (spent / budget.Amount.Amount) * 100;
            var isExceeded = spent > budget.Amount.Amount;

            return new BudgetProgressDto(budget.Amount.Amount, spent, remaining, percentageUsed, isExceeded);

        }
    }
}
