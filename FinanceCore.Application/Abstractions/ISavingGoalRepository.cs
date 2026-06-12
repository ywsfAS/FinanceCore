using FinanceCore.Application.DTOs.Goal;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Goals;

namespace FinanceCore.Application.Abstractions
{
    public interface ISavingsGoalRepository
    {
        Task<SavingsGoal>? GetGoalByIdAsync(Guid id , CancellationToken token);
        Task<IEnumerable<SavingsGoalDto>> GetSavingGoalsFilteredAsync(Guid userId,string? name ,EnCurrency? currency ,EnGoalStatus? status, int page , int pageSize, CancellationToken token);
        Task<SavingsGoal?> GetByIdAndUserIdAsync(Guid userId, Guid id , CancellationToken token);
        Task AddAsync(SavingsGoal goal , CancellationToken token);
        Task UpdateAsync(SavingsGoal goal , CancellationToken token);
        Task DeleteAsync(Guid id , CancellationToken token);

        Task<bool> IsExistsAsync(Guid id , CancellationToken token);
    }
}
