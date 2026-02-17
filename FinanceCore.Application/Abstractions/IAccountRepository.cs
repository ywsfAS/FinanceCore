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
        Task<IEnumerable<Account>> GetByUserIdAsync(Guid id, CancellationToken token = default);
        Task AddAsync(Account account, CancellationToken token = default);
        Task UpdateAsync(Account account, CancellationToken token = default);
        Task DeleteAsync(Account account, CancellationToken token = default);
        
    }
}
