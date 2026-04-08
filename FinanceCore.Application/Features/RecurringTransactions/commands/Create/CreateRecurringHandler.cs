using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs.RecurringTransaction;
using FinanceCore.Application.Events;
using FinanceCore.Domain.RecurringTransaction;
using MediatR;

namespace FinanceCore.Application.Features.RecurringTransactions.commands.Create
{
    public class CreateRecurringHandler
        : IRequestHandler<CreateRecurringCommand, CreateRecurringTransactionDto>
    {
        private readonly IRecurringTransactionRepository _recurringRepository;
        private readonly IMediator _eventBus;

        public CreateRecurringHandler(
            IRecurringTransactionRepository recurringRepository,
            IMediator eventBus)
        {
            _recurringRepository = recurringRepository;
            _eventBus = eventBus;
        }

        public async Task<CreateRecurringTransactionDto> Handle(
            CreateRecurringCommand command,
            CancellationToken cancellationToken)
        {
            var recurring = Domain.RecurringTransaction.RecurringTransaction.Create(
                command.accountId,
                command.categoryId,
                command.amount,
                command.description ?? string.Empty,
                command.type,
                command.startDate,
                command.period,
                command.interval,
                command.endDate
            );

            if (!command.isActive)
                recurring.deactivate();

            await _recurringRepository.AddAsync(recurring);

            await DomainEventDispatcher.DispatchAsync(
                _eventBus,
                recurring,
                cancellationToken
            );

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
