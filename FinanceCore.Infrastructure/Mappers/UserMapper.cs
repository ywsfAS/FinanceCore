using FinanceCore.Application.Models;
using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Users;
namespace FinanceCore.Infrastructure.Mappers
{
    public static class UserMapper
    {
        public static UserModel MapToModel(User user)
        {
  
            return new UserModel { Id = user.Id, Name = user.Name, Email = user.Email.Address, PasswordHash = user.PasswordHash, DefaultCurrencyId = (byte)user.DefaultCurrency, TimeZone = user.TimeZone, CreatedAt = user.CreatedAt, UpdatedAt = user.UpdatedAt };
        }
        public static User MapToDomain(UserModel model)
        {
            return User.Create(model.Id, model.Name,new Email(model.Email), model.PasswordHash, (EnCurrency)model.DefaultCurrencyId, model.TimeZone,model.CreatedAt,model.UpdatedAt);

        }
    }
}
