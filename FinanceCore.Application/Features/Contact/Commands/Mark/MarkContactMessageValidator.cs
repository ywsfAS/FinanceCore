using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Contact.Commands.Mark
{
    public class MarkContactMessageValidator : AbstractValidator<MarkContactMessageCommand>
    {
        public MarkContactMessageValidator() { 
            RuleFor(x => x.msgId).NotEmpty();
        }
    }
}
