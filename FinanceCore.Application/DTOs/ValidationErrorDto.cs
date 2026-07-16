
namespace FinanceCore.Application.DTOs
{
    public class ValidationErrorDto
    {
        public Dictionary<string, string[]> Errors { get; set; } = new();
    }
}
