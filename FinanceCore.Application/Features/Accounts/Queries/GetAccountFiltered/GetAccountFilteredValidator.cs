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
           RuleFor(x => x.UserId).NotEmpty();
           RuleFor(x => x.Type).IsInEnum();
           RuleFor(x => x.Currency).IsInEnum();

            
        }
    }
}
