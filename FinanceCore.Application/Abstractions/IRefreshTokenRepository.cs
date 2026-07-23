
using FinanceCore.Domain.RefreshToken;

namespace FinanceCore.Application.Abstractions
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetByTokenHashAsync(string tokenHash,CancellationToken token = default);
        Task AddAsync(RefreshToken refreshToken,CancellationToken token);
        Task RevokeRefreshTokenAsync(Guid refreshTokenId,DateTime revokedAt,CancellationToken token = default);
        Task RevokeAllForUserAsync(Guid userId, DateTime revokedAt, CancellationToken token = default);

    }
}
