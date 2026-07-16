using FinanceCore.Application.Abstractions;
using MediatR;
using FinanceCore.Domain.Exceptions;

namespace FinanceCore.Application.Features.SavingGoals.Commands.Pause
{
    public class PauseSavingGoalHandler : IRequestHandler<PauseSavingGoalCommand>
    {
        private readonly ISavingsGoalRepository _savingGoalsRepository;
        public PauseSavingGoalHandler(ISavingsGoalRepository savingGoalsRepository)
        {
            _savingGoalsRepository = savingGoalsRepository;
        }
        public async Task Handle(PauseSavingGoalCommand command, CancellationToken token)
        {
            var goal = await _savingGoalsRepository.GetByIdAndUserIdAsync(command.UserId ,command.Id,token);
            if(goal is null)
            {
                throw new GoalNotFoundException(command.Id);
            }
            goal.Pause();
            await _savingGoalsRepository.UpdateAsync(goal,token);
        }
    }
}
