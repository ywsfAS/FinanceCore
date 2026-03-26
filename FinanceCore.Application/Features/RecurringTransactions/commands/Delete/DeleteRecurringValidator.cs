using FinanceCore.Application.Features.Accounts.Commands.Delete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.RecurringTransactions.commands.Delete
{
    public class DeleteRecurringValidator : AbstractValidator<DeleteAccountCommand>
    {
        public DeleteRecurringValidator() { }
    }
} 
