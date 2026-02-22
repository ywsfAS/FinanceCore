using FinanceCore.Domain.Transactions;
using FinanceCore.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Infrastructure.Mappers.ToDB
{
     public static class TransactionMapper
    {
        public static TransactionModel MapToModel(Transaction transaction)
        {
            return new TransactionModel { Id = transaction.Id , AccountId = transaction.AccountId , CategoryId = transaction.CategoryId , Amount = MoneyMapper.MapToModel(transaction.Amount) , Type = (byte)transaction.Type , Date = transaction.Date , Description = transaction.Description , CreatedAt = transaction.CreatedAt , UpdatedAt = transaction.UpdatedAt};

        }

    }
}
