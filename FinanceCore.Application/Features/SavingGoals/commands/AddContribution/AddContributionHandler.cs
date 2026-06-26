using FinanceCore.Application.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceCore.Domain.Exceptions;
using FluentValidation.Validators;
using Microsoft.IdentityModel.Tokens.Experimental;
using FinanceCore.Domain.Common;

namespace FinanceCore.Application.Features.SavingGoals.commands.AddContribution
{
    public class AddContributionHandler : IRequestHandler<AddContributionCommand>
    {
        private readonly ISavingsGoalRepository _savingGoalRepository;
        private readonly IAccountRepository _accountRepository;
        public AddContributionHandler(ISavingsGoalRepository _repo, IAccountRepository accountRepository)
        {
            _savingGoalRepository = _repo;
            _accountRepository = accountRepository;
        }
        public async Task Handle(AddContributionCommand command, CancellationToken token)
        {
            var isAccountExist = await _accountRepository.IsExistsAsync(command.UserId, command.AccountId, token);
            if(!isAccountExist) throw new AccountNotFoundException(command.AccountId);
            var savingGoal = await _savingGoalRepository.GetByIdAndUserIdAsync(command.UserId, command.GoalId, token);
            if(savingGoal is null) throw new GoalNotFoundException(command.GoalId);
            var amount = new Money(command.Amount, command.Currency);
            var contribution = savingGoal.AddContribution(command.AccountId, command.ContributionDate, amount, command.Description);
            await _savingGoalRepository.AddContribution(contribution, token);
            
        }
    }
}
