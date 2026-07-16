
namespace FinanceCore.API.Requests.User
{
    public record UpdateUserRequest (
        string Name,
        string? TimeZone = null);
}
