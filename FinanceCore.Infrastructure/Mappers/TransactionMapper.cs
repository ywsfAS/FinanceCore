using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Transactions;
using FinanceCore.Infrastructure.Models;
using FinanceCore.Domain.Common;
namespace FinanceCore.Infrastructure.Mappers
{
    public static class TransactionMapper
    {
        public static TransactionModel MapToModel(Transaction transaction)
        {
            return new TransactionModel { Id = transaction.Id, AccountId = transaction.AccountId, CategoryId = transaction.CategoryId, Amount = transaction.Amount.Amount,Currency = (byte)transaction.Amount.Currency, Type = (byte)transaction.Type, Date = transaction.Date, Description = transaction.Description, CreatedAt = transaction.CreatedAt, UpdatedAt = transaction.UpdatedAt };

        }
    public static Transaction MapToDomain(TransactionModel model)
        {

            return Transaction.Create(model.Id, model.AccountId, new Money(model.Amount,(EnCurrency)model.Currency), model.CategoryId, (EnTransactionType)model.Type, model.Date, model.Description, null);

        }
    }
}
