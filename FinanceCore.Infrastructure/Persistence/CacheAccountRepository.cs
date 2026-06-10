using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Accounts;
using FinanceCore.Domain.Enums;
using FinanceCore.Infrastructure.Repositories;
using Microsoft.Extensions.Caching.Memory;


namespace FinanceCore.Infrastructure.Persistence
{
    public class CacheAccountRepository : IAccountRepository
    {
        private readonly AccountRepository _accountRepository;
        private readonly IMemoryCache _memoryCache;
        public CacheAccountRepository(AccountRepository accountRepository,IMemoryCache memoryCache)
        {
            _accountRepository = accountRepository;
            _memoryCache = memoryCache;
        }
        public Task<Account?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            string key = $"Account_{id}";

            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                return _accountRepository.GetByIdAsync(id, token);
            });
        }

        public Task<IEnumerable<AccountOptionsDto>?> GetByUserAccountsOptionsAsync(Guid id, CancellationToken token)
        {
            string key = $"Account_options_{id}";

            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                return _accountRepository.GetByUserAccountsOptionsAsync(id,token);
            });

        }

        public  Task<IEnumerable<AccountInfoDto>?> GetAccountInfosAsync(Guid userId , EnAccountType? type , EnCurrency? currency , string? name , CancellationToken token)
        {
            string key = $"Account_infos_{userId}_{type}_{currency}_{name}";

            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                return _accountRepository.GetAccountInfosAsync(userId,type,currency,name,token);
            });
        }
        public Task<IEnumerable<AccountDto>?> GetDtoByNameAsync(Guid id , string name , CancellationToken token)
        {
            string key = $"Account_{id}_{name}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                return _accountRepository.GetDtoByNameAsync(id, name, token);
            });
        }
        public  Task<IEnumerable<Account>?> GetByUserIdAsync(Guid id, CancellationToken token = default)
        {
            string key = $"Accounts_User_{id}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                return _accountRepository.GetByUserIdAsync(id, token);
            });
         }
            
        public  Task<IEnumerable<AccountDto>?> GetDtoByUserIdAsync(Guid id, CancellationToken token = default)
        {
            string key = $"AccountDtos_User_{id}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                return _accountRepository.GetDtoByUserIdAsync(id, token);
            });
            

        }
        public  Task<AccountDto?> GetDtoByIdAndUserIdAsync(Guid userId, Guid id, CancellationToken token = default)
        {
            string key = $"AccountDto_User_{userId}_Account_{id}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                return _accountRepository.GetDtoByIdAndUserIdAsync(userId, id, token);
            });
   

        }
        public  Task<Account?> GetByIdAndUserIdAsync(Guid userId, Guid id, CancellationToken token = default)
        {
            string key = $"Account_User_{userId}_Account_{id}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                return _accountRepository.GetByIdAndUserIdAsync(userId, id, token);
            });
   
        }
         public Task AddAsync(Account account, CancellationToken token = default)
        {
             return _accountRepository.AddAsync(account, token);
  

        }
         public Task UpdateAsync(Account account, CancellationToken token = default)
        {
             return _accountRepository.UpdateAsync(account, token);
     
        }
        public  Task DeleteAsync(Guid accountId, CancellationToken token = default)
        {
            return _accountRepository.DeleteAsync(accountId,token);
        }
        public Task<decimal> GetTotalBalanceAsync(Guid userId, CancellationToken token = default)
        {
            string key = $"TotalBalance_User_{userId}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                return _accountRepository.GetTotalBalanceAsync(userId, token);
            });

        }
        public Task<decimal> GetTotalBalanceByAccountIdAsync(Guid userId,Guid AccountId, CancellationToken token = default)
        {
            string key = $"TotalBalance_User_{userId}_Account{AccountId}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                return _accountRepository.GetTotalBalanceByAccountIdAsync(userId,AccountId, token);
            });

        }
        public Task<bool> IsExists(Guid userId, Guid id, CancellationToken token = default)
        {
            string key = $"AccountExists_User_{userId}_Account_{id}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                return _accountRepository.IsExists(userId, id, token);
            });
           
        }

    }
}
