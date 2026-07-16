using FinanceCore.Domain.Enums;

namespace FinanceCore.Application.DTOs
{
    public record CategoryDto(
        Guid Id,
        Guid UserId,
        string Name,
        CategoryType Type,
        string? Description);

}
