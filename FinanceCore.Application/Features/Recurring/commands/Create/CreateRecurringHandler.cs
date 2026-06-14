using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs.RecurringTransaction;
using FinanceCore.Application.Events;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.RecurringTransaction;
using MediatR;
using FinanceCore.Domain.Exceptions;
using FinanceCore.Domain.Common;

namespace FinanceCore.Application.Features.Recurring.commands.Create
{
    public class CreateRecurringHandler : IRequestHandler<CreateRecurringCommand, RecurringTransactionDto>
    {
        private readonly IRecurringTransactionRepository _recurringRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMediator _eventBus;

        public CreateRecurringHandler(
            IRecurringTransactionRepository recurringRepository,
            ICategoryRepository categoryRepository,
            IAccountRepository accountRepository,
            IMediator eventBus)
        {
            _recurringRepository = recurringRepository;
            _categoryRepository = categoryRepository;
            _accountRepository = accountRepository;
            _eventBus = eventBus;
        }

        public async Task<RecurringTransactionDto> Handle(
            CreateRecurringCommand command,
            CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetCategoryByIdAndUserIdAsync(
                command.UserId,
                command.CategoryId,
                cancellationToken);
            var account = await _accountRepository.GetByIdAndUserIdAsync(command.UserId,command.AccountId);
            if (category is null) throw new CategoryNotFoundException(command.CategoryId);
            if(account is null) throw new AccountNotFoundException(command.AccountId);
            if (!category.IsActive) throw new InactiveCategoryException(command.CategoryId);

            // Derive type from category
            var type = category.Type switch
            {
                CategoryType.Income => EnTransactionType.Income,
                CategoryType.Expense => EnTransactionType.Expense,
                CategoryType.Both => throw new InvalidOperationException(
                    "Category type is ambiguous. Use a category with a specific type."),
                _ => throw new InvalidOperationException("Unknown category type.")
            };
            var amount = new Money(command.Amount, account.Balance.Currency); 
            var recurring = RecurringTransaction.Create(
                command.AccountId,
                command.CategoryId,
                amount,
                command.Description ?? string.Empty,
                type,
                command.StartDate,
                command.Period,
                command.EndDate
            );

            await _recurringRepository.AddAsync(recurring);
            await DomainEventDispatcher.DispatchAsync(_eventBus, recurring, cancellationToken);

            return MapperToDto.MapToDto(recurring);
        }

    }
}
