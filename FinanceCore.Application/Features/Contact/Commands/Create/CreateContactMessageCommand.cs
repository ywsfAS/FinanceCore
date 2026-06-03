using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Contact.Commands.Create
{
    public sealed record CreateContactMessageCommand(string FullName , Email Email ,EnMessageSubject Subject , string Message) : IRequest;
    
}
