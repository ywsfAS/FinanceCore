using FinanceCore.Domain.Enums;

namespace FinanceCore.API.Requests.Category
{
    public record CreateCategoryRequest(
        string Name,
        CategoryType Type,
        string? Description = null);

}
