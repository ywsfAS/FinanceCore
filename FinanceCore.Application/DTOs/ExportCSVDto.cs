
namespace FinanceCore.Application.DTOs
{
    public record ExportCSVDto(byte[] content , string fileName , string contentType);
}
