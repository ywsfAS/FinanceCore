using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Budgets.Commands.Update
{
    public class UpdateBudgetCommandValidator : AbstractValidator<UpdateBudgetCommand>
    {
        public UpdateBudgetCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Amount)
                .GreaterThan(0);

            RuleFor(x => x.Currency)
                .IsInEnum();

            RuleFor(x => x.Period)
                .IsInEnum();

        }
    }

}
