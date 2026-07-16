using FinanceCore.Application.Models;
using FinanceCore.Domain.Common;
using FinanceCore.Domain.Transactions;
namespace FinanceCore.Infrastructure.Mappers
{
    public static class TransactionMapper
    {
        public static TransactionModel MapToModel(Transaction transaction)
        {
            return new TransactionModel { Id = transaction.Id, AccountId = transaction.AccountId, CategoryId = transaction.CategoryId,Type = transaction.Type ,Amount = transaction.Amount.Amount , Currency = transaction.Amount.Currency, ToAccountId = transaction.ToAccountId, Date = transaction.Date, Description = transaction.Description, CreatedAt = transaction.CreatedAt, UpdatedAt = transaction.UpdatedAt };

        }
    public static Transaction MapToDomain(TransactionModel model)
        {

            return Transaction.Load(model.Id, model.AccountId, model.ToAccountId,new Money(model.Amount,model.Currency), model.CategoryId ,model.Type, model.Date, model.Description, model.CreatedAt,model.UpdatedAt);


        }
    }
}
