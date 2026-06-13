using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs.Transaction;
using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Exceptions;
using FinanceCore.Domain.Transactions;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;

namespace FinanceCore.Application.Features.Transactions.Commands.Transactions
{
    public class TransactionHandler : IRequestHandler<TransactionCommand , TransactionDto>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ICategoryRepository _categoryRepositroy;
        private readonly IBudgetRepository _budgetRepository;
        public TransactionHandler(ITransactionRepository transactionRepository,IBudgetRepository budgetRepository, IAccountRepository accountRepository, ICategoryRepository categoryRepositroy)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
            _categoryRepositroy = categoryRepositroy;
            _budgetRepository = budgetRepository;
        }


        public async Task<TransactionDto> Handle(TransactionCommand command , CancellationToken token)
        {
            var account = await _accountRepository.GetDtoByIdAndUserIdAsync(command.UserId, command.AccountId, token);

            if (account is null) throw new AccountNotFoundException(command.AccountId);

            if (command.Type == EnTransactionType.Transfer)
            {
                if (command.ToAccountId is null) throw new AccountNotFoundException(command.ToAccountId.Value);

                var toAccount = await _accountRepository
                    .GetDtoByIdAndUserIdAsync(command.UserId, command.ToAccountId.Value, token);

                if (toAccount is null) throw new AccountNotFoundException(command.ToAccountId.Value);

                var transfer = Transaction.CreateTransfer(
                    fromAccountId: command.AccountId,
                    toAccountId: command.ToAccountId.Value,
                    money: new Money(command.Amount, account.Currency),
                    date: DateTime.UtcNow,
                    description: command.Description
                );

                return await _transactionRepository.TransferAsync(transfer, token);
            }

            if (command.CategoryId is null) throw new InvalidTransactionCategoryException();

            var category = await _categoryRepositroy.GetCategoryByIdAndUserIdAsync(command.UserId, command.CategoryId.Value, token);

            if (category is null) throw new CategoryNotFoundException(command.CategoryId.Value);

            var money = new Money(command.Amount, account.Currency);

            var transaction = Transaction.Create(
                accountId: command.AccountId,
                toAccountId: null,
                amount: money,
                categoryId: command.CategoryId,
                type: command.Type,
                date: DateTime.UtcNow,
                description: command.Description
            );

            return command.Type == EnTransactionType.Expense
                ? await _transactionRepository.ExpenseTransactionAsync(transaction, token)
                : await _transactionRepository.IncomeTransactionAsync(transaction, token);

        }
    }
}
