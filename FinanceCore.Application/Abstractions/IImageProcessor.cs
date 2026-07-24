
namespace FinanceCore.Application.Abstractions
{
    public interface IImageProcessor
    {
        Task<Stream> ProcessAsync(Stream stream , CancellationToken token);
    }
}
