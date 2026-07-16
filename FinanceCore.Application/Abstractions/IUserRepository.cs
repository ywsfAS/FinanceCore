using FinanceCore.Domain.Common;
using FinanceCore.Domain.Users;

namespace FinanceCore.Application.Abstractions
{
   public  interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default);
        Task AddAsync(User user, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task UpdateAsync(User user, CancellationToken cancellationToken = default);
        Task<bool> IsExistsAsync(Guid userId, CancellationToken token = default);
    }
}
