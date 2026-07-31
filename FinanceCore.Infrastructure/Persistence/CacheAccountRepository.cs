using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Accounts;
using FinanceCore.Domain.Enums;
using FinanceCore.Infrastructure.Repositories;

namespace FinanceCore.Infrastructure.Persistence
{
    public class CacheAccountRepository : IAccountRepository
    {
        private readonly AccountRepository _accountRepository;
        private readonly ICacheService _cache;

        private static string Tag(Guid userId) => $"Accounts_{userId}";

        public CacheAccountRepository(AccountRepository accountRepository, ICacheService cache)
        {
            _accountRepository = accountRepository;
            _cache = cache;
        }

        public Task<IEnumerable<AccountOptionsDto>> GetByUserAccountsOptionsAsync(Guid id, int page, int pageSize, CancellationToken token)
        {
            string key = $"AccountOptions_{page}_{pageSize}";
            return _cache.GetOrCreateAsync(Tag(id), key, () => _accountRepository.GetByUserAccountsOptionsAsync(id, page, pageSize, token));
        }

        public Task<IEnumerable<AccountInfoDto>> GetAccountsAsync(Guid userId, EnAccountType? type, EnCurrency? currency, string? name, int page, int pageSize, CancellationToken token = default)
        {
            string key = $"AccountInfos_{type}_{currency}_{name}_{page}_{pageSize}";
            return _cache.GetOrCreateAsync(Tag(userId), key, () => _accountRepository.GetAccountsAsync(userId, type, currency, name, page, pageSize, token));
        }

        public Task<AccountDto?> GetDtoByIdAndUserIdAsync(Guid userId, Guid id, CancellationToken token = default)
        {
            string key = $"AccountDto_{id}";
            return _cache.GetOrCreateAsync(Tag(userId), key, () => _accountRepository.GetDtoByIdAndUserIdAsync(userId, id, token));
        }

        public Task<Account?> GetByIdAndUserIdAsync(Guid userId, Guid id, CancellationToken token = default)
        {
            string key = $"Account_{id}";
            return _cache.GetOrCreateAsync(Tag(userId), key, () => _accountRepository.GetByIdAndUserIdAsync(userId, id, token));
        }

        public Task<bool> IsExistsAsync(Guid userId, Guid id, CancellationToken token = default)
        {
            string key = $"AccountExists_{id}";
            return _cache.GetOrCreateAsync(Tag(userId), key, () => _accountRepository.IsExistsAsync(userId, id, token));
        }

        public async Task AddAsync(Account account, CancellationToken token = default)
        {
            await _accountRepository.AddAsync(account, token);
            await _cache.InvalidateTagAsync(Tag(account.UserId));
        }

        public async Task UpdateAsync(Account account, IUnitOfWork? unitOfWork = null, CancellationToken token = default)
        {
            await _accountRepository.UpdateAsync(account, unitOfWork, token);
            await _cache.InvalidateTagAsync(Tag(account.UserId));
        }

        public async Task DeleteAsync(Guid userId, Guid accountId, CancellationToken token = default)
        {
            await _accountRepository.DeleteAsync(userId, accountId, token);
            await _cache.InvalidateTagAsync(Tag(userId));
        }
    }
}