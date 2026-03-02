using FinanceCore.Application.Abstractions;
using FinanceCore.Application.Features.Categories.Commands.Create;
using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Transactions;
using MediatR;
using FinanceCore.Application.DTOs.Transaction;
using FinanceCore.Domain.Enums;

namespace FinanceCore.Application.Features.Transactions.Commands.Transfer
{
    public class TransferTransactionCommandHandler : IRequestHandler<CreateTransferCommand,CreateTransferDto>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;
        public TransferTransactionCommandHandler(ITransactionRepository transactionRepository,IAccountRepository accountRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
        }

        public async Task<CreateTransferDto?> Handle(CreateTransferCommand command, CancellationToken cancellationToken)
        {
            var account = _accountRepository.GetByIdAndUserIdAsync(command.UserId, command.accountId, cancellationToken);
            if (account == null) { 
                return null;
            }
            var transaction = Transaction.Create(command.accountId,command.ToAccountId,command.amount,null,EnTransactionType.Transfer,DateTime.UtcNow,command.description);
            // Create Transaction
            var result = await _transactionRepository.TransferAsync(transaction, cancellationToken);

            return result;
        }
    }
}
