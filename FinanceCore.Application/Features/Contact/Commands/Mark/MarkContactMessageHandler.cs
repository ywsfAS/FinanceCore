using FinanceCore.Application.Abstractions;
using MediatR;

namespace FinanceCore.Application.Features.Contact.Commands.Mark
{
    public class MarkContactMessageHandler : IRequestHandler<MarkContactMessageCommand>
    {
        private readonly IContactMessageRepository _contactMessageRepository;
        public MarkContactMessageHandler(IContactMessageRepository contactMessageRepository) { 
            _contactMessageRepository = contactMessageRepository;
        }
        public async Task Handle(MarkContactMessageCommand cmd , CancellationToken token)
        {
            var msg = await _contactMessageRepository.GetContactMessageAsync(cmd.msgId, token);
            if (msg is null) return;
            msg.MarkAsProccessed();
            await _contactMessageRepository.MarkAsSeen(msg ,token);
        }

            
    }
}
