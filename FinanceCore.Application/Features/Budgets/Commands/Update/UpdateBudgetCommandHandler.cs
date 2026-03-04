using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Common;
using MediatR;
using FinanceCore.Domain.Exceptions;
namespace FinanceCore.Application.Features.Budgets.Commands.Update
{
    public class UpdateBudgetCommandHandler : IRequestHandler<UpdateBudgetCommand>
    {
        private readonly IBudgetRepository _budgetRepository;

        public UpdateBudgetCommandHandler(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public async Task Handle(UpdateBudgetCommand command, CancellationToken cancellationToken)
        {
            var budget = await _budgetRepository.GetByIdAndUserIdAsync(command.UserId,command.Id, cancellationToken);

            if (budget is null)
                throw new BudgetNotFoundException(command.Id);

            budget.UpdateAmount(new Money(command.Amount));
            budget.UpdateName(command.Name);
            budget.ExtendPeriod(command.Period);
   
            await _budgetRepository.UpdateAsync(budget, cancellationToken);
        }
    }
}
