using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using MediatR;

namespace FinanceCore.Application.Features.Report.ContributionsTrend
{
    public class ContributionsTrendHandler : IRequestHandler<ContributionsTrendQuery,ContributionsTrendDto?>
    {
        private readonly ISavingsGoalRepository _savingGoalsRepository;
        public ContributionsTrendHandler(ISavingsGoalRepository savedGoalsRepository)
        {
            _savingGoalsRepository = savedGoalsRepository;
        }
        public async Task<ContributionsTrendDto?> Handle(ContributionsTrendQuery query , CancellationToken token)
        {
            return await _savingGoalsRepository.GetContributionsTrendAsync(query.UserId,query.LastNMonth,token);

        }
    }
}
