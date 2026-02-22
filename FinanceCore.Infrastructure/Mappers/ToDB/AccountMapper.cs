using FinanceCore.Domain.Accounts;
using FinanceCore.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Infrastructure.Mappers.ToDB
{
    public static class AccountMapper
    {
        public static AccountModel MapToModel(Account account)
        {
            return new AccountModel { Id = account.Id, UserId = account.UserId, Name = account.Name, Type = (byte)account.Type, Balance = MoneyMapper.MapToModel(account.Balance), InitialBalance = MoneyMapper.MapToModel(account.InitialBalance), Currency = (byte)account.Currency, IsActive = account.IsActive, CreatedAt = account.CreatedAt};


        }
    }
}
