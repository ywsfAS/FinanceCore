using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Accounts.Queries.GetAccountFiltered
{
    public class GetAccountFilteredValidator : AbstractValidator<GetAccountFilteredQuery>
    {
        public GetAccountFilteredValidator() { 
           RuleFor(x => x.userId).NotEmpty();
           RuleFor(x => x.type).IsInEnum();
           RuleFor(x => x.currency).IsInEnum();

            
        }
    }
}
