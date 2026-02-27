using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Accounts.Queries.GetBalanceById
{
    public class GetBalanceByIdValidator : AbstractValidator<GetBalanceByIdQuery>
    {
        public GetBalanceByIdValidator() { 
            RuleFor(x => x.AccountId).NotEmpty();
       
        }
    }
}
