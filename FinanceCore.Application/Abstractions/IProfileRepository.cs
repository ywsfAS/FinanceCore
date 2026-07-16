using FinanceCore.Domain.Profile;

namespace FinanceCore.Application.Abstractions
{
    public interface IProfileRepository
    {
        Task<bool> ExistsAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Profile profile , CancellationToken token);
        Task<bool> ExistsByUserIdAsync(Guid userId);
        Task<Profile?> GetProfileByUserIdAsync(Guid userId);
        Task AddAsync(Profile profile , CancellationToken token);
    }
}
