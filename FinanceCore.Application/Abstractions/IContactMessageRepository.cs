using FinanceCore.Domain.ContactMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Abstractions
{
     public interface IContactMessageRepository
    {
        Task AddAsync(ContactMessage msg, CancellationToken token);
        Task MarkAsSeen(ContactMessage msg,CancellationToken token);
        Task<ContactMessage?> GetContactMessageAsync(Guid msgId, CancellationToken token);

    }
}
