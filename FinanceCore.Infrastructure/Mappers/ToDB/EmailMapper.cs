using FinanceCore.Infrastructure.Models;
using FinanceCore.Domain.Common;
namespace FinanceCore.Infrastructure.Mappers.ToDB
{
    static class EmailMapper
    {
        public static EmailModel MapToModel(Email email )
        {
            return new EmailModel{ Email = email.Address };

        }
    }
}
