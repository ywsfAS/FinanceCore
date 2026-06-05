using FinanceCore.Application.Abstractions;
using FinanceCore.Application.Features.SavingGoals.commands.Pause;
using FinanceCore.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.SavingGoals.commands.Resume
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
            var goal = await _savingGoalsRepository.GetByIdAndUserIdAsync(command.Id, command.UserId, token);
            if (goal is null)
            {
                throw new GoalNotFoundException(command.Id);
            }
            goal.Resume();
            await _savingGoalsRepository.UpdateAsync(goal, token);
        }
    }
}
