
namespace FinanceCore.Application.Abstractions
{
    public interface IRefreshTokenHasher
    {
        string Hash(string token);
    }
}
