using FinanceCore.Application.Abstractions;
using MediatR;
using FinanceCore.Domain.Exceptions;
using FinanceCore.Application.Events;

namespace FinanceCore.Application.Features.SavingGoals.Commands.Pause
{
    public class PauseSavingGoalHandler : IRequestHandler<PauseSavingGoalCommand>
    {
        private readonly ISavingsGoalRepository _savingGoalsRepository;
        private readonly IMediator _eventBus;
        public PauseSavingGoalHandler(ISavingsGoalRepository savingGoalsRepository , IMediator eventBus )
        {
            _savingGoalsRepository = savingGoalsRepository;
            _eventBus = eventBus;
        }
        public async Task Handle(PauseSavingGoalCommand command, CancellationToken token)
        {
            var goal = await _savingGoalsRepository.GetByIdAndUserIdAsync(command.UserId ,command.Id,token);
            if(goal is null)
            {
                throw new GoalNotFoundException(command.Id);
            }
            goal.Pause();
            await DomainEventDispatcher.DispatchAsync(_eventBus, goal,token); 
            await _savingGoalsRepository.UpdateAsync(goal,token);
        }
    }
}
