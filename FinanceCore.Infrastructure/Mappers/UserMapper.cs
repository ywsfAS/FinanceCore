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
  
            return new UserModel { Id = user.Id, Name = user.Name, Email = user.Email.Address, PasswordHash = user.PasswordHash, TimeZone = user.TimeZone, CreatedAt = user.CreatedAt, UpdatedAt = user.UpdatedAt , Role = (int)user.Role , LockedUntil = user.LockedUntil , FailedLoginAttempts = user.FailedLoginAttempts};
        }
        public static User MapToDomain(UserModel model)
        {
            return User.Load(model.Id, model.Name,new Email(model.Email), model.PasswordHash,(UserRole)model.Role,model.FailedLoginAttempts,model.LockedUntil,model.TimeZone,model.CreatedAt,model.UpdatedAt);

        }
    }
}
