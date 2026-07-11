using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Transactions.Export
{
    public class ExportCSVValidator : AbstractValidator<ExportCSVQuery>
    {
        public ExportCSVValidator() { 
        
            RuleFor(x => x.Page).GreaterThan(0).NotEmpty();
            RuleFor(x => x.PageSize).GreaterThan(0).NotEmpty();
        
        }
    }
}
