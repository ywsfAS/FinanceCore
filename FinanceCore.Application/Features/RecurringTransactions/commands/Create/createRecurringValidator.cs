using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.RecurringTransactions.commands.Create
{
    public class createRecurringValidator : AbstractValidator<CreateRecurringCommand>
    {
        public createRecurringValidator() { }
    }
}
