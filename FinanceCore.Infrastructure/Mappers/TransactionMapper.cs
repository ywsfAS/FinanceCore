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
            return new TransactionModel { Id = transaction.Id, AccountId = transaction.AccountId, CategoryId = transaction.CategoryId, Amount = transaction.Amount.Amount, ToAccountId = transaction.ToAccountId, Date = transaction.Date, Description = transaction.Description, CreatedAt = transaction.CreatedAt, UpdatedAt = transaction.UpdatedAt };

        }
    public static Transaction MapToDomain(TransactionModel model)
        {

            return Transaction.Create(model.Id, model.AccountId, model.ToAccountId,new Money(model.Amount), model.CategoryId ,(EnTransactionType)model.Type, model.Date, model.Description, model.CreatedAt,model.UpdatedAt);

        }
    }
}
