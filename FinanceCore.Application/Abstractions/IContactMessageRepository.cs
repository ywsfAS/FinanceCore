using FinanceCore.Domain.ContactMessage;

namespace FinanceCore.Application.Abstractions
{
     public interface IContactMessageRepository
    {
        Task AddAsync(ContactMessage msg, CancellationToken token);
        Task MarkAsSeen(ContactMessage msg,CancellationToken token);
        Task<ContactMessage?> GetContactMessageAsync(Guid msgId, CancellationToken token);

    }
}
