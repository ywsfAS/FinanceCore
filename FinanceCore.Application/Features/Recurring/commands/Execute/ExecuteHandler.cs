using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Exceptions;
using FinanceCore.Domain.Transactions;
using MediatR;

namespace FinanceCore.Application.Features.Recurring.Commands.Execute
{
    public class ExecuteHandler : IRequestHandler<ExecuteCommand>
    {
        private readonly IRecurringTransactionRepository _recurringTransactionRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ExecuteHandler(IRecurringTransactionRepository recurring , ITransactionRepository transactionRepository, IAccountRepository accountRepository, IUnitOfWork unitOfWork )
        {
            _recurringTransactionRepository = recurring;
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(ExecuteCommand command , CancellationToken token)
        {
            var recurring = await _recurringTransactionRepository.GetByIdAsync(command.UserId,command.Id);
            if (recurring is null) throw new RecurringTransactionNotFoundException(command.UserId,command.Id);
            if(recurring.Status != EnRecurringTransactionStatus.Due) throw new RecurringTransactionNotDueException(command.UserId,command.Id,"Cannot execute a not due recurring transaction ");

            // Create transaction & apply it to account 
            var account = await _accountRepository.GetAccountByIdAsync(recurring.AccountId);
            var transaction = Transaction.Create(recurring.AccountId,null,recurring.Amount,recurring.CategoryId,recurring.Type,recurring.NextExecutionAt);
            recurring.MarkAsExecuted(DateTime.UtcNow);
            account.ApplyTransaction(recurring.Amount,recurring.Type);
            await _unitOfWork.BeginAsync(token);
            try
            {
                await _accountRepository.UpdateAsync(account,_unitOfWork,token);
                await _transactionRepository.AddAsync(transaction, _unitOfWork, token);
                await _recurringTransactionRepository.UpdateAsync(recurring, _unitOfWork, token);

                await _unitOfWork.CommitAsync(token);
            }
            catch
            {
                await _unitOfWork.RollBackAsync(token);
                throw;
            }

        }
    }
}
