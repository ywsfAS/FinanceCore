using FinanceCore.Domain.Goals;

namespace FinanceCore.Application.Abstractions
{
    public interface ISavingsGoalRepository
    {
        Task<SavingsGoal?> GetByIdAsync(Guid id);
        Task<IEnumerable<SavingsGoal>> GetByUserIdAsync(Guid userId);

        Task AddAsync(SavingsGoal goal);
        Task UpdateAsync(SavingsGoal goal);
        Task DeleteAsync(Guid id);

        Task<bool> ExistsAsync(Guid id);
    }
}
