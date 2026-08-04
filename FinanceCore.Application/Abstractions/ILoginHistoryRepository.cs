using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.LoginHistory;

namespace FinanceCore.Application.Abstractions
{
    public interface ILoginHistoryRepository
    {
        Task AddAsync(LoginHistory history ,IUnitOfWork? unitOfWork = null, CancellationToken token = default);
        Task<PagedResult<LoginHistoryDto>> GetLoginHistoriesFilteredAsync(Guid userId, EnLoginStatus? status, string? search, DateTime? From, DateTime? To,int Page = 1 , int PageSize = 10, CancellationToken token = default);
    }
}
