using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs.Goal;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.SavingGoals.Queries.GetSavingGoalsByStatus
{
    public class GetSavingGoalByStatusHandler : IRequestHandler<GetSavingGoalByStatusQuery,IEnumerable<SavingsGoalDto>?>
    {
        private readonly ISavingsGoalRepository _savingGoalsRepository;
        public GetSavingGoalByStatusHandler(ISavingsGoalRepository savingsGoalRepository) {
            _savingGoalsRepository = savingsGoalRepository; 
        }
        public async Task<IEnumerable<SavingsGoalDto>?> Handle(GetSavingGoalByStatusQuery query , CancellationToken token)
        {
            var goals = await _savingGoalsRepository.GetDtosByUserIdAndStatusAsync(query.UserId,query.Status,query.Page , query.PageSize, token);
            return goals;
        }
    }
}
