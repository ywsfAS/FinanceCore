using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs.Goal;
using FinanceCore.Application.Events;
using FinanceCore.Domain.Exceptions;
using MediatR;

namespace FinanceCore.Application.Features.Goals.Commands.Update
{
    public class UpdateSavingsGoalHandler : IRequestHandler<UpdateSavingsGoalCommand, SavingsGoalDto>
    {
        private readonly ISavingsGoalRepository _goalRepository;
        private readonly IMediator _eventBus;

        public UpdateSavingsGoalHandler(ISavingsGoalRepository goalRepository, IMediator eventBus)
        {
            _goalRepository = goalRepository;
            _eventBus = eventBus;
        }

        public async Task<SavingsGoalDto> Handle(UpdateSavingsGoalCommand command, CancellationToken token)
        {
            var goal = await _goalRepository.GetGoalByIdAsync(command.Id,token);
            if (goal == null)
                throw new GoalNotFoundException(command.Id);
            // Update domain entity
            goal.UpdateDetails(
                command.Name,
                command.TargetAmount,
                command.TargetDate,
                command.Description
            );

            await _goalRepository.UpdateAsync(goal,token);

            await DomainEventDispatcher.DispatchAsync(_eventBus, goal, token);

            return new SavingsGoalDto(
                goal.Id,
                goal.UserId,
                goal.Name,
                goal.Description,
                goal.TargetAmount.Amount,
                goal.CurrentAmount.Amount,
                goal.TargetAmount.Currency,
                goal.TargetDate,
                goal.Status,
                goal.CompletedAt
            );
        }
    }
}
