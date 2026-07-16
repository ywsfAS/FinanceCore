using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Exceptions;
using MediatR;

namespace FinanceCore.Application.Features.SavingGoals.Commands.Resume
{
    public class ResumeSavingGoalHandler : IRequestHandler<ResumeSavingGoalCommand>
    {
        private readonly ISavingsGoalRepository _savingGoalsRepository;
        public ResumeSavingGoalHandler(ISavingsGoalRepository savingGoalsRepository)
        {
            _savingGoalsRepository = savingGoalsRepository;
        }
        public async Task Handle(ResumeSavingGoalCommand command, CancellationToken token)
        {
            var goal = await _savingGoalsRepository.GetByIdAndUserIdAsync(command.UserId, command.Id, token);
            if (goal is null)
            {
                throw new GoalNotFoundException(command.Id);
            }
            goal.Resume();
            await _savingGoalsRepository.UpdateAsync(goal, token);
        }
    }
}
