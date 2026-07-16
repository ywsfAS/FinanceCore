using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using MediatR;

namespace FinanceCore.Application.Features.Contact.Commands.Create
{
    public sealed record CreateContactMessageCommand(string FullName , Email Email ,EnMessageSubject Subject , string Message) : IRequest;
    
}
