using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Accounts.Queries.GetAccountByName
{
    public class GetAccountByNameValidator : AbstractValidator<GetAccountByNameQuery>
    {
        public GetAccountByNameValidator() { 
            RuleFor(X => X.userId).NotEmpty();
            RuleFor(X => X.name).NotEmpty();
        }
    }
}
