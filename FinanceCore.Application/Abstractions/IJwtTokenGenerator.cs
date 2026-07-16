using FinanceCore.Domain.Users;

namespace FinanceCore.Application.Abstractions
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
