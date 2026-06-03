using FinanceCore.Application.Models;
using FinanceCore.Domain.Common;
using FinanceCore.Domain.ContactMessage;
using FinanceCore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Infrastructure.Mappers
{
    public class ContactMessageMapper
    {
        public static ContactMessageModel MapToModel(ContactMessage contactMessage)
        {
            return new ContactMessageModel
            {
               Id = contactMessage.Id,
               FullName = contactMessage.FullName,
               Email = contactMessage.Email.Address,
               Subject = (byte)contactMessage.Subject,
               Message = contactMessage.Message,
               IsProccessed = contactMessage.IsProccessed,
               CreatedAt = contactMessage.CreatedAt,
            };
        }
        public static ContactMessage MapToDomain(ContactMessageModel model)
        {
            return new ContactMessage(model.FullName, new Email(model.Email), (EnMessageSubject)model.Subject, model.Message, model.CreatedAt);

        }
    }
}
