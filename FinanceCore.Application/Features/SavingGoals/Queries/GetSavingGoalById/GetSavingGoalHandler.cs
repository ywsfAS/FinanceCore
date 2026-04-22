using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs.Goal;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.SavingGoals.Queries.GetSavingGoalById
{
    public class GetSavingGoalHandler : IRequestHandler<GetSavingGoalQuery,SavingsGoalDto>
    {
        private readonly ISavingsGoalRepository _savingGoalRepository;
        public GetSavingGoalHandler(ISavingsGoalRepository savingsGoalRepository)
        {
               _savingGoalRepository = savingsGoalRepository; 
        }

        public async Task<SavingsGoalDto> Handle(GetSavingGoalQuery query, CancellationToken token)
        {
            var goal = await _savingGoalRepository.GetByIdAndUserIdAsync(query.userId, query.id);
            return new SavingsGoalDto(goal.Id,goal.UserId,goal.Name,goal.Description,goal.TargetAmount,goal.CurrentAmount,goal.TargetDate,goal.Status,goal.CreatedAt,goal.UpdatedAt,goal.CompletedAt);

        }
    }
}
