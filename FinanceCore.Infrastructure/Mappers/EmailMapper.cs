using FinanceCore.Application.Models;
using FinanceCore.Domain.Common;
namespace FinanceCore.Infrastructure.Mappers
{
    static class EmailMapper
    {
        public static EmailModel MapToModel(Email email )
        {
            return new EmailModel{ Email = email.Address };

        }
        public static Email MapToDomain(EmailModel model)
        {
            return new Email(model.Email);
        }
    }
}
