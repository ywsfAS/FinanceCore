using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Exceptions;
using MediatR;


namespace FinanceCore.Application.Features.SavingGoals.Commands.Cancel
{
    public class CancelSavingGoalHandler : IRequestHandler<CancelSavingGoalCommand>
    {

        private readonly ISavingsGoalRepository _savingGoalsRepository;
        public CancelSavingGoalHandler(ISavingsGoalRepository savingGoalsRepository)
        {
            _savingGoalsRepository = savingGoalsRepository;
        }
        public async Task Handle(CancelSavingGoalCommand command, CancellationToken token)
        {
            var goal = await _savingGoalsRepository.GetByIdAndUserIdAsync(command.UserId ,command.Id,token);
            if(goal is null)
            {
                throw new GoalNotFoundException(command.Id);
            }
            goal.Cancel();
            await _savingGoalsRepository.UpdateAsync(goal,token);
        }
    }
}
