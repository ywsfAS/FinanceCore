using FinanceCore.Domain.Enums;

namespace FinanceCore.API.Requests.Profile
{
    public record createProfileRequest(string firstName, string lastName, string bio, string? avatarUrl, EnCurrency curreny);
}
