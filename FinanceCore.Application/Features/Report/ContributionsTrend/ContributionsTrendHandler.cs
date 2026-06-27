using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
