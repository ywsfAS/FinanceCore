using FinanceCore.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
