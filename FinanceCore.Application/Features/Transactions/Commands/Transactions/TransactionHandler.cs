using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs.Transaction;
using FinanceCore.Application.Events;
using FinanceCore.Domain.Accounts;
using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Exceptions;
using FinanceCore.Domain.Transactions;
using MediatR;

namespace FinanceCore.Application.Features.Transactions.Commands.Transactions
{
    public class TransactionHandler
        : IRequestHandler<TransactionCommand, TransactionDto>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _eventBus;

        public TransactionHandler(
            ITransactionRepository transactionRepository,
            IAccountRepository accountRepository,
            ICategoryRepository categoryRepository,
            IMediator bus,
            IUnitOfWork unitOfWork)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _eventBus = bus;
        }

        public async Task<TransactionDto> Handle(
            TransactionCommand command,
            CancellationToken token)
        {
            var account =
                await _accountRepository.GetByIdAndUserIdAsync(
                    command.UserId,
                    command.AccountId,
                    token);

            if (account is null)
                throw new AccountNotFoundException(command.AccountId);

            if (command.Type == EnTransactionType.Transfer)
            {
                return await HandleTransferAsync(
                    command,
                    account,
                    token);
            }

            if (command.CategoryId is null)
                throw new InvalidTransactionCategoryException();

            var category =
                await _categoryRepository.GetCategoryByIdAndUserIdAsync(
                    command.UserId,
                    command.CategoryId.Value,
                    token);

            if (category is null)
                throw new CategoryNotFoundException(
                    command.CategoryId.Value);

            var money = new Money(
                command.Amount,
                account.Balance.Currency);

            account.ApplyTransaction(
                money,
                command.Type);

            var transaction = Transaction.Create(
                accountId: command.AccountId,
                toAccountId: null,
                amount: money,
                categoryId: command.CategoryId,
                type: command.Type,
                date: DateTime.UtcNow,
                description: command.Description);

            await _unitOfWork.BeginAsync(token);

            try
            {
                await _accountRepository.UpdateAsync(
                    account,
                    _unitOfWork,
                    token);

                await _transactionRepository.AddAsync(
                    transaction,
                    _unitOfWork,
                    token);

                // Dispatch events
                await Task.WhenAll(new Entity[] {account,transaction}.Select(e => DomainEventDispatcher.DispatchAsync(_eventBus,e,token)));


                await _unitOfWork.CommitAsync(token);
            }
            catch
            {
                await _unitOfWork.RollBackAsync(token);
                throw;
            }

            return await _transactionRepository
                .GetDtoByIdAndUserId(
                    command.UserId,
                    transaction.Id,
                    token);
        }

        private async Task<TransactionDto> HandleTransferAsync(
            TransactionCommand command,
            Account account,
            CancellationToken token)
        {
            if (command.ToAccountId is null)
                throw new InvalidOperationException(
                    "Destination account is required for a transfer.");

            var toAccount =
                await _accountRepository.GetByIdAndUserIdAsync(
                    command.UserId,
                    command.ToAccountId.Value,
                    token);

            if (toAccount is null)
                throw new AccountNotFoundException(
                    command.ToAccountId.Value);

            var money = new Money(
                command.Amount,
                account.Balance.Currency);

            account.TransferTo(
                toAccount,
                money);

            var transfer = Transaction.CreateTransfer(
                fromAccountId: command.AccountId,
                toAccountId: command.ToAccountId.Value,
                money: money,
                date: DateTime.UtcNow,
                description: command.Description);

            await _unitOfWork.BeginAsync(token);

            try
            {
                await _accountRepository.UpdateAsync(
                    account,
                    _unitOfWork,
                    token);

                await _accountRepository.UpdateAsync(
                    toAccount,
                    _unitOfWork,
                    token);

                await _transactionRepository.AddAsync(
                    transfer,
                    _unitOfWork,
                    token);

                await Task.WhenAll(new Entity[] { account, transfer}.Select(e => DomainEventDispatcher.DispatchAsync(_eventBus, e, token)));

                await _unitOfWork.CommitAsync(token);
            }
            catch
            {
                await _unitOfWork.RollBackAsync(token);
                throw;
            }

            return await _transactionRepository
                .GetDtoByIdAndUserId(
                    command.UserId,
                    transfer.Id,
                    token);
        }
    }
}