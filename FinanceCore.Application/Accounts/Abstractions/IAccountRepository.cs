
using FinanceCore.Domain.Accounts;

namespace FinanceCore.Application.Accounts.Abstractions
{
    public interface IAccountRepository
    {
        Account? GetById(Guid accountId);
        void Add( Account account);
        void Update(Account account);
    }
}
