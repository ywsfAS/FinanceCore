using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Enums;
using MediatR;
using FinanceCore.Domain.Exceptions;
using FinanceCore.Domain.Common;
using FinanceCore.Domain.Accounts;
using FinanceCore.Domain.Transactions;

namespace FinanceCore.Application.Features.Accounts.Commands.Reconcile
{
    public class ReconcileAccountHandler : IRequestHandler<ReconcileAccountCommand,ReconciliationDto>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IReconciliationRepository _reconciliationRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReconcileAccountHandler(IReconciliationRepository conciliationRepository,IUnitOfWork unitOfWork,ICategoryRepository categoryRepository ,ITransactionRepository transactionRepository , IAccountRepository accountRepository)
        {
            _reconciliationRepository = conciliationRepository;
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork; 
            _categoryRepository = categoryRepository;
        }

        public async Task<ReconciliationDto> Handle(ReconcileAccountCommand command , CancellationToken token)
        {
            var account = await _accountRepository.GetByIdAndUserIdAsync(command.UserId,command.AccountId,token);
            if (account is null) throw new AccountNotFoundException(command.AccountId); 
            if(account.Type != EnAccountType.Cash) throw new ReconcileAccountException(command.AccountId);

            Money expected = account.Balance;
            Money actual = new Money(command.ActualBalance,expected.Currency);
            decimal diff = actual.Amount - expected.Amount;
            var transactionType = diff > 0 ? EnTransactionType.CreditAdjustment : EnTransactionType.DebitAdjustment;  
            Money difference = new Money(Math.Abs(diff), expected.Currency);

            var reconciliation = Reconciliation.Create(account.Id, expected, actual, reason: command.Reason , notes : command.Notes);

            Transaction transaction;
            await _unitOfWork.BeginAsync(token);
            try
            {
                if (!command.CreateAdjustment)
                {
                    await _reconciliationRepository.AddAsync(reconciliation,_unitOfWork,token);
                    await _unitOfWork.CommitAsync(token);
                    return new ReconciliationDto(reconciliation.Id, expected, actual, difference, false, null);
                }
                // TODO: Add global categories for adjustment transactions!
                transaction = Transaction.Create(account.Id,null,difference,null,transactionType,DateTime.UtcNow);
                account.ApplyTransaction(actual,transactionType);

                await _accountRepository.UpdateAsync(account, _unitOfWork, token);
                await _transactionRepository.AddAsync(transaction,_unitOfWork, token);
                await _reconciliationRepository.AddAsync(reconciliation,_unitOfWork,token);

                await _unitOfWork.CommitAsync(token);
            }
            catch
            {
                await _unitOfWork.RollBackAsync(token);
                throw;
            }
            return new ReconciliationDto(reconciliation.Id, expected, actual, difference,true,transaction.Id);
        }
    }
}
