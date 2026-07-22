using FinanceCore.Application.Abstractions;
using FinanceCore.Application.Events;
using FinanceCore.Domain.Exceptions;
using MediatR;


namespace FinanceCore.Application.Features.SavingGoals.Commands.Cancel
{
    public class CancelSavingGoalHandler : IRequestHandler<CancelSavingGoalCommand>
    {

        private readonly ISavingsGoalRepository _savingGoalsRepository;
        private readonly IMediator _eventBus;
        public CancelSavingGoalHandler(ISavingsGoalRepository savingGoalsRepository , IMediator eventBus)
        {
            _savingGoalsRepository = savingGoalsRepository;
            _eventBus = eventBus;
        }
        public async Task Handle(CancelSavingGoalCommand command, CancellationToken token)
        {
            var goal = await _savingGoalsRepository.GetByIdAndUserIdAsync(command.UserId ,command.Id,token);
            if(goal is null)
            {
                throw new GoalNotFoundException(command.Id);
            }
            goal.Cancel();
            await DomainEventDispatcher.DispatchAsync(_eventBus,goal,token);
            await _savingGoalsRepository.UpdateAsync(goal,token);
        }
    }
}
