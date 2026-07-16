using FluentValidation;

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
