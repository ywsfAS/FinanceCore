using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Transactions.Commands.Income
{
    public class IncomeValidator : AbstractValidator<IncomeCommand>
    {
        public IncomeValidator() { 
            RuleFor(x => x.AccountId).NotEmpty();
            RuleFor(x => x.Amount).GreaterThan(0);
            RuleFor(x => x.CategoryId).NotEmpty();
        
        
        }
    }
}
