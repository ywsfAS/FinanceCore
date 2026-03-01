using FinanceCore.Application.DTOs.Transaction;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Transactions.Queries.GetTansactionsByAccountId
{
    public class GetTransactionsByAccountIdValidator : AbstractValidator<GetTransactionsByAccountIdQuery>
    {
        public GetTransactionsByAccountIdValidator() {
            RuleFor(x => x.Page).GreaterThan(0).NotEmpty();
            RuleFor(x => x.PageSize).GreaterThan(0).NotEmpty();
            RuleFor(x => x.Id).NotEmpty();

        }
    }
}
