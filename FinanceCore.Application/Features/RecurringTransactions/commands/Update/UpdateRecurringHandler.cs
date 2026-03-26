using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs.RecurringTransaction;
using FinanceCore.Application.Events;
using FinanceCore.Domain.RecurringTransaction;
using MediatR;

namespace FinanceCore.Application.Features.RecurringTransaction.Commands.Update
{
    public class UpdateRecurringHandler : IRequestHandler<UpdateRecurringCommand, CreateRecurringTransactionDto>
    {
        private readonly IRecurringTransactionRepository _recurringRepository;
        private readonly IMediator _eventBus;

        public UpdateRecurringHandler(
            IRecurringTransactionRepository recurringRepository,
            IMediator eventBus)
        {
            _recurringRepository = recurringRepository;
            _eventBus = eventBus;
        }

        public async Task<CreateRecurringTransactionDto> Handle(
            UpdateRecurringCommand command,
            CancellationToken cancellationToken)
        {
            // Load existing entity
            var recurring = await _recurringRepository.GetByIdAsync(command.Id);
            if (recurring == null)
                throw new InvalidOperationException("Recurring transaction not found.");

            // Update properties
            recurring.UpdateDetails(
                command.AccountId,
                command.CategoryId,
                command.Amount,
                command.Description ?? string.Empty,
                command.Type,
                command.StartDate,
                command.Period,
                command.Interval,
                command.EndDate
            );

            // Handle activation
            if (command.IsActive)
                recurring.activate();
            else
                recurring.deactivate();

            await _recurringRepository.UpdateAsync(recurring);

            await DomainEventDispatcher.DispatchAsync(_eventBus, recurring, cancellationToken);

            // Return DTO
            return new CreateRecurringTransactionDto
            {
                AccountId = recurring.accountId,
                CategoryId = recurring.categoryId,
                Amount = recurring.amount,
                Description = recurring.description,
                Type = recurring.type,
                StartDate = recurring.startDate,
                EndDate = recurring.endDate,
                Period = recurring.period,
                Interval = recurring.interval
            };
        }
    }
}
