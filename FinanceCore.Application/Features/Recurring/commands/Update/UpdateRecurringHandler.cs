using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs.RecurringTransaction;
using FinanceCore.Application.Events;
using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Exceptions;
using FinanceCore.Domain.RecurringTransaction;
using MediatR;

namespace FinanceCore.Application.Features.Recurring.Commands.Update
{
    public class UpdateRecurringHandler : IRequestHandler<UpdateRecurringCommand, RecurringTransactionDto>
    {
        private readonly IRecurringTransactionRepository _recurringRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMediator _eventBus;

        public UpdateRecurringHandler(
            IRecurringTransactionRepository recurringRepository,
            ICategoryRepository categoryRepository,
            IAccountRepository accountRepository,
            IMediator eventBus)
        {
            _recurringRepository = recurringRepository;
            _categoryRepository = categoryRepository;
            _eventBus = eventBus;
        }

        public async Task<RecurringTransactionDto> Handle(
            UpdateRecurringCommand command,
            CancellationToken cancellationToken)
        {

            var account = await _accountRepository.GetByIdAndUserIdAsync(command.UserId,command.AccountId);
            if (account is null) throw new AccountNotFoundException(command.AccountId);
            var recurring = await _recurringRepository.GetByIdAsync(account.UserId,command.Id);
            if(recurring is null) throw new InvalidOperationException("Recurring transaction not found.");
            var category = await _categoryRepository.GetCategoryByIdAndUserIdAsync(
                command.UserId,
                command.CategoryId,
                cancellationToken);
            if(category is null) throw new CategoryNotFoundException(command.CategoryId);
            if (!category.IsActive) throw new InactiveCategoryException(command.CategoryId);

            var type = category.Type switch
            {
                CategoryType.Income => EnTransactionType.Income,
                CategoryType.Expense => EnTransactionType.Expense,
                CategoryType.Both => throw new InvalidOperationException(
                    "Category type is ambiguous. Use a category with a specific type."),
                _ => throw new InvalidOperationException("Unknown category type.")
            };
            var amount = new Money(command.Amount, account.Balance.Currency);
            recurring.UpdateDetails(
                command.AccountId,
                command.CategoryId,
                amount,
                command.Description ?? string.Empty,
                type,
                command.StartDate,
                command.Period,
                command.EndDate
            );

            if (command.IsActive)
                recurring.Activate();
            else
                recurring.Deactivate();

            await _recurringRepository.UpdateAsync(recurring);
            await DomainEventDispatcher.DispatchAsync(_eventBus, recurring, cancellationToken);

            return MapperToDto.MapToDto(recurring);
        }

    }
}

