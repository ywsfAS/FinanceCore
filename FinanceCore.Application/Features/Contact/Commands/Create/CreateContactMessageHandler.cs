using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.ContactMessage;
using MediatR;

namespace FinanceCore.Application.Features.Contact.Commands.Create
{
     public class CreateContactMessageHandler :IRequestHandler<CreateContactMessageCommand>
    {
        private readonly IContactMessageRepository _contactMessageRepository;
        public CreateContactMessageHandler(IContactMessageRepository contactMessageRepository) { 
            _contactMessageRepository = contactMessageRepository;
        }
        public async Task Handle(CreateContactMessageCommand cmd , CancellationToken token)
        {
            var message = new ContactMessage(cmd.FullName,cmd.Email,cmd.Subject,cmd.Message,DateTime.UtcNow);
            await _contactMessageRepository.AddAsync(message,token);
        }

    }
}
