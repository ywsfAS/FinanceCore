using FinanceCore.Infrastructure.Models;
using FinanceCore.Domain.Common;

namespace FinanceCore.Infrastructure.Mappers.ToDomain
{
    static class EmailMapper
    {
        public static Email MapToDomain(EmailModel model)
        {
            return new Email(model.Email);
        }
    }
}
