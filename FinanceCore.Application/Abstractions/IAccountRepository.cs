using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Abstractions
{
    public interface IAccountRepository
    {
        Task<Account?> GetByIdAsync(Guid id , CancellationToken token = default);
        Task<IEnumerable<Account>?> GetByUserIdAsync(Guid id, CancellationToken token = default);
        Task<IEnumerable<AccountDto>?> GetDtoByUserIdAsync(Guid id, CancellationToken token = default);
        Task<AccountDto?> GetDtoByIdAndUserIdAsync(Guid userId , Guid id, CancellationToken token = default);
        Task<Account?> GetByIdAndUserIdAsync(Guid userId, Guid id, CancellationToken token = default);
        Task AddAsync(Account account, CancellationToken token = default);
        Task UpdateAsync(Account account, CancellationToken token = default);
        Task DeleteAsync(Guid accountId, CancellationToken token = default);
        Task<decimal> GetTotalBalanceAsync(Guid userId,CancellationToken token = default);
        Task<decimal> GetTotalBalanceByAccountIdAsync(Guid userId,Guid AccountId,CancellationToken token = default);
        Task<bool> IsExists(Guid userId,Guid id,CancellationToken token = default);
    }
}
