using FinanceCore.Application.Abstractions;

namespace FinanceCore.Infrastructure.Auth
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Hash(string Password)
        {

            return BCrypt.Net.BCrypt.HashPassword(Password);
        }
        public bool Verify(string password, string HashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password,HashedPassword);
        }
    }
}
