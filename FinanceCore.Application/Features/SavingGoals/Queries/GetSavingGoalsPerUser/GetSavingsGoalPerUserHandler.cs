using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs.Goal;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.SavingGoals.Queries.GetSavingGoalsPerUser
{
    public class GetSavingsGoalPerUserHandler : IRequestHandler<GetSavingsGoalPerUserQuery,IEnumerable<SavingsGoalDto>>
    {
        private ISavingsGoalRepository _savingGoalRepository;
        public GetSavingsGoalPerUserHandler(ISavingsGoalRepository savingGoalRepository)
        {
            _savingGoalRepository = savingGoalRepository;
        }
        public async Task<IEnumerable<SavingsGoalDto>> Handle(GetSavingsGoalPerUserQuery query , CancellationToken token)
        {
            var goals = await _savingGoalRepository.GetByUserIdAsync(query.userId);
            var result = goals.Select((goal) => new SavingsGoalDto(goal.Id, goal.UserId, goal.Name, goal.Description, goal.TargetAmount, goal.CurrentAmount, goal.TargetDate, goal.Status, goal.CreatedAt, goal.UpdatedAt, goal.CompletedAt));
            return result;
        }
    }
}
