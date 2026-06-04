using FinanceCore.Domain.Enums;

namespace FinanceCore.API.Requests.Profile
{
    public record CreateProfileRequest(string FirstName, string LastName, string Bio, EnCurrency Currency);
}
