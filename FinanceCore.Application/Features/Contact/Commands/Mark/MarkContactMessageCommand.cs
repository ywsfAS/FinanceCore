using MediatR;

namespace FinanceCore.Application.Features.Contact.Commands.Mark
{
    public sealed record MarkContactMessageCommand(Guid msgId) : IRequest;
}
