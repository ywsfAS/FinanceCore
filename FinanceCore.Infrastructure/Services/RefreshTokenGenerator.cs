using FinanceCore.Application.Abstractions;
using System.Security.Cryptography;

namespace FinanceCore.Infrastructure.Services
{
    public class RefreshTokenGenerator : IRefreshTokenGenerator
    {
        public string GenerateRefreshToken() {

            byte[] randomBytes = RandomNumberGenerator.GetBytes(64);
            return Convert.ToBase64String(randomBytes);
        }
    }
}
