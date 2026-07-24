
namespace FinanceCore.Application.Abstractions
{
    public interface IImageStorage
    {
        Task<string> SaveAsync(Stream stream ,CancellationToken token = default);
        Task DeleteAsync(string path,CancellationToken token);
    }
}
