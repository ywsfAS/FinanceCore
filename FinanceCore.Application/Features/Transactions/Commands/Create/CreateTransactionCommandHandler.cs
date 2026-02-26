using FinanceCore.Application.Abstractions;
using FinanceCore.Application.Features.Categories.Commands.Create;
using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Transactions;
using MediatR;

namespace FinanceCore.Application.Features.Transactions.Commands.Create
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand,TransactionDto>
    {
        private readonly ITransactionRepository _transactionRepository;

        public CreateTransactionCommandHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<TransactionDto> Handle(CreateTransactionCommand command, CancellationToken cancellationToken)
        {
            var transaction = Transaction.Create(command.accountId,command.ToAccountId,command.amount,command.CategoryId,command.type,DateTime.UtcNow,command.description);
        
            await _transactionRepository.AddAsync(transaction, cancellationToken);

            return new TransactionDto(transaction.Id,transaction.AccountId,transaction.ToAccountId,transaction.CategoryId,transaction.Amount,transaction.Type,transaction.Date,transaction.Description);
        }
    }
}
