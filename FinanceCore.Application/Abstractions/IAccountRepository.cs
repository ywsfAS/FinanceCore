using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Accounts;
using FinanceCore.Domain.Enums;

namespace FinanceCore.Application.Abstractions
{
    public interface IAccountRepository
    {
        Task<IEnumerable<AccountOptionsDto>> GetByUserAccountsOptionsAsync(Guid id,int page , int pageSize, CancellationToken token = default);
        Task<AccountDto?> GetDtoByIdAndUserIdAsync(Guid userId , Guid id, CancellationToken token = default);
        Task<Account?> GetByIdAndUserIdAsync(Guid userId, Guid id, CancellationToken token = default);
        Task AddAsync(Account account, CancellationToken token = default);
        Task UpdateAsync(Account account,IUnitOfWork? unitOfWork = null, CancellationToken token = default);
        Task DeleteAsync(Guid userId , Guid accountId, CancellationToken token = default);
        Task<bool> IsExistsAsync(Guid userId,Guid id,CancellationToken token = default);
        Task<IEnumerable<AccountInfoDto>> GetAccountsAsync(Guid userId , EnAccountType? type , EnCurrency? currency , string? name,int page = 1 , int pageSize = 10 , CancellationToken token = default);
        Task<IDictionary<Guid,Account>> GetUserOwnedAccountsAsync(Guid userId,CancellationToken token = default);
        Task UpdateAccountsAsync(IEnumerable<Account> accounts, IUnitOfWork? unitOfWork = null, CancellationToken token = default);

        Task<Account?> GetAccountByIdAsync(Guid accountId , CancellationToken token = default);
    }
}
