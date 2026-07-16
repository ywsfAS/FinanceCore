
namespace FinanceCore.Application.DTOs
{
    public record UserDto(
        Guid Id,
        string Name,
        string Email,
        string? TimeZone);

}
