using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Accounts.Queries.GetAccountByUserOptions
{
   public class GetAccountsOptionsValidator : AbstractValidator<GetAccountsOptionsQuery>
   {
        public GetAccountsOptionsValidator() {
            RuleFor(x => x.userId).NotEmpty();
        }
   }
}
