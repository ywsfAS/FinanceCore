using FinanceCore.Application.Abstractions;
using FinanceCore.Application.Features.Categories.Commands.Create;
using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Transactions;
using MediatR;
using FinanceCore.Application.DTOs.Transaction;
using FinanceCore.Domain.Enums;

namespace FinanceCore.Application.Features.Transactions.Commands.Transfer
{
    public class TransferTransactionCommandHandler : IRequestHandler<TransferTransactionCommand,CreateTransferDto>
    {
        private readonly ITransactionRepository _transactionRepository;
        public TransferTransactionCommandHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<CreateTransferDto> Handle(TransferTransactionCommand command, CancellationToken cancellationToken)
        { 
            var transaction = Transaction.Create(command.accountId,command.ToAccountId,command.amount,null,EnTransactionType.Transfer,DateTime.UtcNow,command.description);
            // Create Transaction
            var result = await _transactionRepository.TransferAsync(transaction, cancellationToken);

            return result;
        }
    }
}
