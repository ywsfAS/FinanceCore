using FinanceCore.Application.Abstractions;
using MediatR;
using FinanceCore.Domain.Exceptions;
using FinanceCore.Domain.Common;
using FinanceCore.Application.Events;

namespace FinanceCore.Application.Features.SavingGoals.Commands.AddContribution
{
    public class AddContributionHandler : IRequestHandler<AddContributionCommand>
    {
        private readonly ISavingsGoalRepository _savingGoalRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMediator _eventBus;
        public AddContributionHandler(ISavingsGoalRepository _repo, IAccountRepository accountRepository , IMediator eventBus)
        {
            _savingGoalRepository = _repo;
            _accountRepository = accountRepository;
            _eventBus = eventBus;
        }
        public async Task Handle(AddContributionCommand command, CancellationToken token)
        {
            var isAccountExist = await _accountRepository.IsExistsAsync(command.UserId, command.AccountId, token);
            if(!isAccountExist) throw new AccountNotFoundException(command.AccountId);
            var savingGoal = await _savingGoalRepository.GetByIdAndUserIdAsync(command.UserId, command.GoalId, token);
            if(savingGoal is null) throw new GoalNotFoundException(command.GoalId);
            var amount = new Money(command.Amount, command.Currency);
            var contribution = savingGoal.AddContribution(command.AccountId, command.ContributionDate, amount, command.Description);
            await DomainEventDispatcher.DispatchAsync(_eventBus, contribution,token);
            await _savingGoalRepository.AddContributionAsync(contribution, token);
            
        }
    }
}
