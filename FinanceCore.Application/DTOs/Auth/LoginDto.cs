
namespace FinanceCore.Application.DTOs.Auth
{
    public record LoginDto(
        Guid Id,
        string Email,
        string Token,
        string refreshToken,
        DateTime TokenExpiresAt
    );
}
