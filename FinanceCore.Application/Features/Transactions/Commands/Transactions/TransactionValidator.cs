using FinanceCore.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Transactions.Commands.Transactions
{
    public class TransactionValidator : AbstractValidator<TransactionCommand>
    {
        public TransactionValidator()
        {
            RuleFor(x => x.UserId).NotNull().NotEmpty();
            RuleFor(x => x.TransactionDate).NotEmpty();
            When(x => x.Type == EnTransactionType.Transfer, () =>
            {
                RuleFor(x => x.ToAccountId).NotNull().NotEmpty();
                RuleFor(x => x.CategoryId).Null().Empty();

            }
            );
            When(x => x.Type != EnTransactionType.Transfer, () =>
            {
                RuleFor(x => x.CategoryId).NotNull().NotEmpty();
                RuleFor(x => x.ToAccountId).Null().Empty();
            });
             
        }
    }
}
