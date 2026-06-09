using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Accounts.Queries.GetAccountByUserOptions
{
   public class GetAccountByUserOptionsValidator : AbstractValidator<GetAccountByUserOptionsQuery>
   {
        public GetAccountByUserOptionsValidator() {
            RuleFor(x => x.userId).NotEmpty();
        }
   }
}
