using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.Events;
using FinanceCore.Domain.Budgets;
using FinanceCore.Domain.Exceptions;
using MediatR;
namespace FinanceCore.Application.Features.Budgets.Commands.Create
{
    public class CreateBudgetCommandHandler : IRequestHandler<CreateBudgetCommand, BudgetDto>
    {
        private readonly IBudgetRepository _budgetRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMediator _eventBus;

        public CreateBudgetCommandHandler(
            IBudgetRepository budgetRepository,
            IMediator eventBus,
            ICategoryRepository categoryRepository)
        {
            _budgetRepository = budgetRepository;
            _categoryRepository = categoryRepository;
            _eventBus = eventBus;
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
            await DomainEventDispatcher.DispatchAsync(_eventBus,budget, cancellationToken); 
            return new BudgetDto(budget.Id,budget.UserId,budget.Name,budget.CategoryId,budget.Amount.Amount,budget.Amount.Currency,budget.Period,budget.StartDate,budget.EndDate);
           
        }
    }

}
