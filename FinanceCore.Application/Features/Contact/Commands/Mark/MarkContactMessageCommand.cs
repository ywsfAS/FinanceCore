using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Contact.Commands.Mark
{
    public sealed record MarkContactMessageCommand(Guid msgId) : IRequest;
}
