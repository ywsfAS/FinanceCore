using FinanceCore.Domain.Enums;

namespace FinanceCore.API.Requests.Profile
{
    public record UpdateProfileRequest(string FirstName , string LastName , string Bio , EnCurrency Currency );
}
