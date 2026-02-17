using FinanceCore.Application.Abstractions;
using FinanceCore.Application.Features.Categories.Commands.Create;
using FinanceCore.Domain.Common;
using FinanceCore.Domain.Transactions;
using MediatR;

namespace FinanceCore.Application.Features.Transactions.Commands.Create
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, Guid>
    {
        private readonly ITransactionRepository _transactionRepository;

        public CreateTransactionCommandHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<Guid> Handle(CreateTransactionCommand command, CancellationToken cancellationToken)
        {
            var transaction = Transaction.Create(command.accountId,new Money(command.amount,command.currency),command.CategoryId,command.type,DateTime.UtcNow,command.description,command.notes);
        
            await _transactionRepository.AddAsync(transaction, cancellationToken);

            return transaction.Id;
        }
    }
}
