using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Common;
using FinanceCore.Domain.Users;
using FinanceCore.Infrastructure.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace FinanceCore.Infrastructure.Persistence
{
    public class CacheUserRepository : IUserRepository
    {
        private readonly UserRepository _userRepository;
        private readonly IMemoryCache _memoryCache;
        public CacheUserRepository(UserRepository userRepository, IMemoryCache memoryCache)
        {
            _userRepository = userRepository;
            _memoryCache = memoryCache;
        }
        public Task<User?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            var key = $"User_{id}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _userRepository.GetByIdAsync(id, token);
            });
        }
        public Task UpdateLoginSecurityStateAsync(Guid userId , int failedLoginAttempts , DateTime? lockedUntil, CancellationToken token = default)
        {
            return _userRepository.UpdateLoginSecurityStateAsync(userId,failedLoginAttempts,lockedUntil,token);
        }
        public Task<User?> GetByEmailAsync(Email email, CancellationToken token = default)
        {
            var key = $"User_Email_{email}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _userRepository.GetByEmailAsync(email, token);
            });
        }
        public Task AddAsync(User user, CancellationToken token = default)
        {
            return _userRepository.AddAsync(user, token);
        }
        public Task UpdateAsync(User user, CancellationToken token = default)
        {
            return _userRepository.UpdateAsync(user, token);
        }
        public Task DeleteAsync(Guid id, CancellationToken token = default)
        {
           return _userRepository.DeleteAsync(id, token);
        }
        public Task<bool> IsExistsAsync(Guid userId, CancellationToken token = default)
        {
            var key = $"UserExists_{userId}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _userRepository.IsExistsAsync(userId, token);
            });
        }
    }
}
