using FinanceCore.Application.Abstractions;
using System.Security.Cryptography;
using System.Text;

namespace FinanceCore.Infrastructure.Services
{
    public class RefreshTokenHasher : IRefreshTokenHasher
    {
        public string Hash(string token)
        {
            var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(token);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
