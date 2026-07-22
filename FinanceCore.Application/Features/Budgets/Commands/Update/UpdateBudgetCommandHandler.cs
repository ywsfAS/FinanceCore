using FinanceCore.Application.Abstractions;
using MediatR;
using FinanceCore.Domain.Exceptions;
using FinanceCore.Application.Events;
namespace FinanceCore.Application.Features.Budgets.Commands.Update
{
    public class UpdateBudgetCommandHandler : IRequestHandler<UpdateBudgetCommand>
    {
        private readonly IBudgetRepository _budgetRepository;
        private readonly IMediator _eventBus;

        public UpdateBudgetCommandHandler(IBudgetRepository budgetRepository , IMediator eventBus)
        {
            _budgetRepository = budgetRepository;
            _eventBus = eventBus;
        }

        public async Task Handle(UpdateBudgetCommand command, CancellationToken cancellationToken)
        {
            var budget = await _budgetRepository.GetByIdAndUserIdAsync(command.UserId,command.Id, cancellationToken);

            if (budget is null)
                throw new BudgetNotFoundException(command.Id);

            budget.UpdateAmount(command.Amount);
            budget.UpdateName(command.Name);
            budget.ExtendPeriod(command.Period);
            await DomainEventDispatcher.DispatchAsync(_eventBus, budget,cancellationToken); 
            await _budgetRepository.UpdateAsync(budget, cancellationToken);
        }
    }
}
