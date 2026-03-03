using FinanceCore.Domain.Exceptions;
using FinanceCore.Application.Abstractions;
using MediatR;
namespace FinanceCore.Application.Features.Transactions.Commands.Delete
{

    public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand>
    {
        private readonly ITransactionRepository _transactionRepository;

        public DeleteTransactionCommandHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task Handle(DeleteTransactionCommand command, CancellationToken cancellationToken)
        {
            var result = await _transactionRepository.IsExists(command.UserId,command.Id, cancellationToken);

            if (!result)
                throw new TransactionNotFoundException(command.Id);

            await _transactionRepository.DeleteAsync(command.Id, cancellationToken);
        }
    }

}
