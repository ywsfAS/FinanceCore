using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Users;
using FinanceCore.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Infrastructure.Mappers.ToDomain
{
    static class UserMapper
    {
        public static User MapToDomain(UserModel model)
        {
            return User.Create(model.Id,model.Name,EmailMapper.MapToDomain(model.Email),model.PasswordHash ,(EnCurrency)model.DefaultCurrency,model.TimeZone);

        } 
    }
}
