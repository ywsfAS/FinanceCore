using FinanceCore.Domain.Users;
using FinanceCore.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Infrastructure.Mappers.ToDB
{
    public static class UserMapper
    {
        public static UserModel MapToModel(User user)
        {
  
            return new UserModel { Id = user.Id, Name = user.Name, Email = EmailMapper.MapToModel(user.Email), PasswordHash = user.PasswordHash, DefaultCurrency = (byte)user.DefaultCurrency, TimeZone = user.TimeZone, CreatedAt = user.CreatedAt, UpdatedAt = user.UpdatedAt };
        }
    }
}
