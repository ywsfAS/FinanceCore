using FinanceCore.Application.Abstractions;
using FinanceCore.Application.Events;
using FinanceCore.Domain.Exceptions;
using MediatR;

namespace FinanceCore.Application.Features.SavingGoals.Commands.Resume
{
    public class ResumeSavingGoalHandler : IRequestHandler<ResumeSavingGoalCommand>
    {
        private readonly ISavingsGoalRepository _savingGoalsRepository;
        private readonly IMediator _eventBus;
        public ResumeSavingGoalHandler(ISavingsGoalRepository savingGoalsRepository , IMediator eventBus)
        {
            _savingGoalsRepository = savingGoalsRepository;
            _eventBus = eventBus;
        }
        public async Task Handle(ResumeSavingGoalCommand command, CancellationToken token)
        {
            var goal = await _savingGoalsRepository.GetByIdAndUserIdAsync(command.UserId, command.Id, token);
            if (goal is null)
            {
                throw new GoalNotFoundException(command.Id);
            }
            goal.Resume();
            await DomainEventDispatcher.DispatchAsync(_eventBus,goal,token);
            await _savingGoalsRepository.UpdateAsync(goal, token);
        }
    }
}
