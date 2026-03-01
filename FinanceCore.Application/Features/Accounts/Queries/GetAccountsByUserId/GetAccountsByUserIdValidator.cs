using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Accounts.Queries.GetAccountsByUserId
{
    public class GetAccountsByUserIdValidator : AbstractValidator<GetAccountsByUserIdQuery>
    {
        public GetAccountsByUserIdValidator() {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
