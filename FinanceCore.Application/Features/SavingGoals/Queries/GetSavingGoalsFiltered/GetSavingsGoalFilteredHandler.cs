using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs.Goal;
using MediatR;

namespace FinanceCore.Application.Features.SavingGoals.Queries.GetSavingGoalsFiltered
{
    public class GetSavingsGoalFilteredHandler : IRequestHandler<GetSavingsGoalFilteredQuery,IEnumerable<SavingsGoalDto>>
    {
        private ISavingsGoalRepository _savingGoalRepository;
        public GetSavingsGoalFilteredHandler(ISavingsGoalRepository savingGoalRepository)
        {
            _savingGoalRepository = savingGoalRepository;
        }
        public async Task<IEnumerable<SavingsGoalDto>> Handle(GetSavingsGoalFilteredQuery query , CancellationToken token)
        {
            var goals = await _savingGoalRepository.GetSavingGoalsFilteredAsync(query.userId,query.name,query.Currency,query.Status,query.Page,query.PageSize, token);
            return goals;
        }
    }
}
