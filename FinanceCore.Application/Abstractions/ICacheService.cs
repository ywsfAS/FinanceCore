
namespace FinanceCore.Application.Abstractions
{
    public interface ICacheService
    {
        Task<T> GetOrCreateAsync<T>(string tag, string key, Func<Task<T>> factory, TimeSpan? ttl = null);
        Task InvalidateTagAsync(string tag);
        Task InvalidateKey(string key);
    }
}
