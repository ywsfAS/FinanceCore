using FinanceCore.Application.Abstractions;
using FinanceCore.Application.Events;
using FinanceCore.Domain.Goals;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Goals.Commands.Delete
{
    public class DeleteSavingsGoalHandler : IRequestHandler<DeleteSavingsGoalCommand>
    {
        private readonly ISavingsGoalRepository _goalRepository;
        private readonly IMediator _eventBus;

        public DeleteSavingsGoalHandler(ISavingsGoalRepository goalRepository, IMediator eventBus)
        {
            _goalRepository = goalRepository;
            _eventBus = eventBus;
        }

        public async Task Handle(DeleteSavingsGoalCommand command, CancellationToken cancellationToken)
        {
            var goal = await _goalRepository.GetByIdAsync(command.Id);
            if (goal == null)
                throw new InvalidOperationException("Savings goal not found.");

            await _goalRepository.DeleteAsync(command.Id);

            await DomainEventDispatcher.DispatchAsync(_eventBus, goal, cancellationToken);

        }
    }
}
