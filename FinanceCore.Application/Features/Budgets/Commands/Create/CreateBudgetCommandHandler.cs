using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Budgets;
using FinanceCore.Domain.Exceptions;
using MediatR;
namespace FinanceCore.Application.Features.Budgets.Commands.Create
{
    public class CreateBudgetCommandHandler : IRequestHandler<CreateBudgetCommand, BudgetDto>
    {
        private readonly IBudgetRepository _budgetRepository;
        private readonly ICategoryRepository _categoryRepository;

        public CreateBudgetCommandHandler(
            IBudgetRepository budgetRepository,
            ICategoryRepository categoryRepository)
        {
            _budgetRepository = budgetRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<BudgetDto> Handle(CreateBudgetCommand command, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetCategoryByIdAndUserIdAsync(command.UserId,command.CategoryId, cancellationToken);

            if (category is null)
                throw new CategoryNotFoundException(command.CategoryId);
            if (category.Type != Domain.Enums.CategoryType.Expense)
                throw new InvalidCategoryTypeException(category.Type); 

          
            var budget = Budget.Create(
                command.UserId,
                command.CategoryId,
                command.name,
                command.Amount,
                command.Period,
                command.StartDate
                );

            await _budgetRepository.AddAsync(budget, cancellationToken);

            return new BudgetDto(budget.Id,budget.UserId,budget.Name,budget.CategoryId,budget.Amount.Amount,budget.Amount.Currency,budget.Period,budget.StartDate,budget.EndDate);
           
        }
    }

}
