using FinanceCore.Application.Abstractions;
using MediatR;
using FinanceCore.Domain.Exceptions;
namespace FinanceCore.Application.Features.Budgets.Commands.Delete
{
    public class DeleteBudgetCommandHandler : IRequestHandler<DeleteBudgetCommand>
    {
        private readonly IBudgetRepository _budgetRepository;

        public DeleteBudgetCommandHandler(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public async Task Handle(DeleteBudgetCommand command, CancellationToken cancellationToken)
        {
            var budget = await _budgetRepository.GetByIdAndUserIdAsync(command.UserId,command.Id, cancellationToken);

            if (budget is null)
                throw new BudgetNotFoundException(command.Id);

            await _budgetRepository.DeleteAsync(budget, cancellationToken);
        }
    }
}
