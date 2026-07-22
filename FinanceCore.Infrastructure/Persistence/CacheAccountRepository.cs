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
        public Task<IEnumerable<AccountOptionsDto>> GetByUserAccountsOptionsAsync(Guid id,int page ,int pageSize , CancellationToken token)
        {
            string key = $"Account_options_{id}";

            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                return _accountRepository.GetByUserAccountsOptionsAsync(id,page,pageSize,token);
            });

        }

        public  Task<IEnumerable<AccountInfoDto>> GetAccountsAsync(Guid userId , EnAccountType? type , EnCurrency? currency , string? name ,int page, int pageSize, CancellationToken token = default)
        {
            string key = $"Account_infos_{userId}_{type}_{currency}_{name}";

            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                return _accountRepository.GetAccountsAsync(userId,type,currency,name,page,pageSize,token);
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
         public Task UpdateAsync(Account account, IUnitOfWork? unitOfWork = null,CancellationToken token = default)
        {
             return _accountRepository.UpdateAsync(account,unitOfWork,token);
     
        }
        public  Task DeleteAsync(Guid userId, Guid accountId, CancellationToken token = default)
        {
            return _accountRepository.DeleteAsync(userId, accountId,token);
        }
        public Task<bool> IsExistsAsync(Guid userId, Guid id, CancellationToken token = default)
        {
            string key = $"AccountExists_User_{userId}_Account_{id}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                return _accountRepository.IsExistsAsync(userId, id, token);
            });
           
        }

    }
}
