using FinanceCore.Domain.Enums;

namespace FinanceCore.API.Requests
{
    public sealed record GetLoginHistoryRequest(
        EnLoginStatus? Status = null,
        string? Search = null,
        DateTime? From = null,
        DateTime? To = null,
        int Page = 1,
        int PageSize = 10);
}
