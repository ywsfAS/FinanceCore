using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Budgets;
using FinanceCore.Domain.Common;
using MediatR;
using FinanceCore.Domain.Exceptions;
namespace FinanceCore.Application.Features.Budgets.Commands.Create
{
    public class CreateBudgetCommandHandler : IRequestHandler<CreateBudgetCommand, Guid>
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

        public async Task<Guid> Handle(CreateBudgetCommand command, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(command.CategoryId, cancellationToken);

            if (category is null)
                throw new CategoryNotFoundException(command.CategoryId);

            var budget = Budget.Create(
                command.UserId,
                command.CategoryId,
                command.name,
                command.Currency,
                new Money(command.Amount),
                command.Period,
                command.StartDate
                );

            await _budgetRepository.AddAsync(budget, cancellationToken);

            return budget.Id;
        }
    }

}
