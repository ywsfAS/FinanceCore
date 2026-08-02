using FluentValidation;

namespace FinanceCore.Application.Features.Transactions.Commands.TransactionImports
{
    public class Validator : AbstractValidator<Command>
    {
        public Validator() {

            RuleFor(x => x.Stream).NotEmpty();
            RuleFor(x => x.Type).IsInEnum();
        
        }
    }
}
