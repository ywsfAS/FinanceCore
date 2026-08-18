using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Common;
using FinanceCore.Domain.Users;
using FinanceCore.Infrastructure.Repositories;
namespace FinanceCore.Infrastructure.Persistence
{
    public class CacheUserRepository : IUserRepository
    {
        private readonly UserRepository _repo;
        private readonly ICacheService _cache;
        private static string Tag(Guid userId) => $"Users_{userId}";
        public CacheUserRepository(UserRepository userRepository, ICacheService cache)
        {
            _repo = userRepository;
            _cache = cache;
        }
        public Task<User?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            var key = $"User_{id}";
            return _cache.GetOrCreateAsync(Tag(id), key, () => _repo.GetByIdAsync(id, token));
        }
        public async Task UpdateLoginSecurityStateAsync(Guid userId , int failedLoginAttempts , DateTime? lockedUntil, CancellationToken token = default)
        {
            await _repo.UpdateLoginSecurityStateAsync(userId,failedLoginAttempts,lockedUntil,token);
            await _cache.InvalidateTagAsync(Tag(userId));
        }
        public Task<User?> GetByEmailAsync(Email email, CancellationToken token = default)
        {
            return _repo.GetByEmailAsync(email, token);
        }
        public async Task AddAsync(User user, CancellationToken token = default)
        {
            await _repo.AddAsync(user, token);
            await _cache.InvalidateTagAsync(Tag(user.Id));
        }
        public async Task UpdateAsync(User user, CancellationToken token = default)
        {
            await _repo.UpdateAsync(user, token);
            await _cache.InvalidateTagAsync(Tag(user.Id));
        }
        public async Task DeleteAsync(Guid id, CancellationToken token = default)
        {
            await _repo.DeleteAsync(id, token);
            await _cache.InvalidateTagAsync(Tag(id));
        }
        public Task<bool> IsExistsAsync(Guid userId, CancellationToken token = default)
        {
            var key = $"UserExists_{userId}";
            return _cache.GetOrCreateAsync(Tag(userId), key, () => _repo.IsExistsAsync(userId, token));
        }
        public Task<PagedResult<UserDto>> GetUsersAsync(
        string? search,
        string? role, bool? isLocked,
        int page, int pageSize, CancellationToken token = default)
        {
            return _repo.GetUsersAsync(search,role,isLocked,page,pageSize,token);
        }
    }
}
