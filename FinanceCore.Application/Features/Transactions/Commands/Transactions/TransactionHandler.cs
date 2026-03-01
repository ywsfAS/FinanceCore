using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs.Transaction;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Exceptions;
using FinanceCore.Domain.Transactions;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;

namespace FinanceCore.Application.Features.Transactions.Commands.Transactions
{
    public class TransactionHandler : IRequestHandler<TransactionCommand , CreateTransactionDto>
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


        public async Task<CreateTransactionDto> Handle(TransactionCommand command , CancellationToken token)
        {
            var account = await _accountRepository.GetByIdAsync(command.AccountId, token);
            if (account == null)
            {
                throw new AccountNotFoundException(command.AccountId);
            }
            var category = await _categoryRepositroy.GetByIdAsync(command.CategoryId, token);
            if (category == null)
            {
                throw new CategoryNotFoundException(command.CategoryId);
            }
            // Get Budget
            var budget = await _budgetRepository.GetByCategoryIdAsync(account.UserId,category.Id,command.TransactionDate , command.TransactionDate);
            var spent = await _transactionRepository.GetTotalSpentAsync(
                budget.CategoryId,
                budget.StartDate,
                budget.EndDate,
                (byte)EnTransactionType.Expense
            );
            var isExceeded = spent > budget.Amount;
            if (isExceeded)
            {
                throw new BudgetExceededException(budget.Id , category.Name , budget.Amount , spent);
            }
            var transaction = Transaction.Create(command.AccountId, null, command.Amount, command.CategoryId,command.Type, DateTime.UtcNow, command.Description);
            if (command.Type == EnTransactionType.Expense)
            {
                return await _transactionRepository.ExpenseAsync(transaction, token);

            }
            return await _transactionRepository.IncomeAsync(transaction, token);


        }
    }
}
