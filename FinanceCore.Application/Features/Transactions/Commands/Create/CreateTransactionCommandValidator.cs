using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Transactions.Commands.Create
{
    public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
    {
        public CreateTransactionCommandValidator() {

            RuleFor(x => x.accountId)
            .NotEmpty();
            RuleFor(x => x.amount)
            .NotEmpty().LessThan(0);
            RuleFor(x => x.type)
            .IsInEnum();
            RuleFor(x => x.currency)
            .IsInEnum();
        }
    }
}
