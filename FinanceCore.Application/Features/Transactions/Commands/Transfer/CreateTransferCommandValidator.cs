using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Transactions.Commands.Transfer
{
    public class CreateTransferCommandValidator : AbstractValidator<TransferTransactionCommand>
    {
        public CreateTransferCommandValidator() {

            RuleFor(x => x.accountId)
            .NotEmpty();
            RuleFor(x => x.ToAccountId).NotEmpty();
            RuleFor(x => x.amount)
            .NotEmpty().GreaterThan(0);
  
           }
    }
}
