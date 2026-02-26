using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using MediatR;
using FinanceCore.Domain.Exceptions;
namespace FinanceCore.Application.Features.Budgets.Queries.GetBudgetById
{
    public class GetBudgetByIdQueryHandler : IRequestHandler<GetBudgetByIdQuery, BudgetDto>
    {
        private readonly IBudgetRepository _budgetRepository;

        public GetBudgetByIdQueryHandler(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public async Task<BudgetDto> Handle(GetBudgetByIdQuery query, CancellationToken cancellationToken)
        {
            var budget = await _budgetRepository.GetByIdAsync(query.Id, cancellationToken);

            if (budget is null)
                throw new BudgetNotFoundException(query.Id);

            return new BudgetDto(
                budget.Id,
                budget.UserId,
                budget.CategoryId,
                budget.Amount.Amount,
                budget.Currency,
                budget.Period,
                budget.StartDate,
                budget.EndDate);
        }
    }
}
