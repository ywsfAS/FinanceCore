using FluentValidation;

namespace FinanceCore.Application.Features.Transactions.Commands.Delete
{
    public class DeleteTransactionCommandValidator : AbstractValidator<DeleteTransactionCommand>
    {
        public DeleteTransactionCommandValidator() { 
            RuleFor(x => x.Id).NotEmpty();  
        }
    }
}
