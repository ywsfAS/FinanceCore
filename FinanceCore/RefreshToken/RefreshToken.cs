
using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.RefreshToken
{
    public sealed class RefreshToken : Entity
    {

        public Guid UserId { get; private set; }

        public string TokenHash { get; private set; } = null!;

        public DateTime ExpiresAt { get; private set; }

        public DateTime? RevokedAt { get; private set; }

        public string? DeviceLabel { get; private set; }

        public string? UserAgent { get; private set; }

        public DateTime? LastUsedAt { get; private set; }

        public DateTime CreatedAt { get; private set; }

        private RefreshToken()
        {
        }

        private RefreshToken(
            Guid id,
            Guid userId,
            string tokenHash,
            DateTime expiresAt,
            string? deviceLabel,
            string? userAgent)
        {
            Id = id;
            UserId = userId;
            TokenHash = tokenHash;
            ExpiresAt = expiresAt;
            DeviceLabel = deviceLabel;
            UserAgent = userAgent;
            CreatedAt = DateTime.UtcNow;
        }

        public static RefreshToken Create(
            Guid userId,
            string tokenHash,
            DateTime expiresAt,
            string? deviceLabel = null,
            string? userAgent = null)
        {
            return new RefreshToken(
                Guid.NewGuid(),
                userId,
                tokenHash,
                expiresAt,
                deviceLabel,
                userAgent);
        }

        public bool IsExpired(DateTime utcNow)
        {
            return ExpiresAt <= utcNow;
        }

        public bool IsRevoked()
        {
            return RevokedAt.HasValue;
        }

        public bool IsActive(DateTime utcNow)
        {
            return !IsRevoked() && !IsExpired(utcNow);
        }

        public void Revoke(DateTime utcNow)
        {
            if (RevokedAt.HasValue)
            {
                return;
            }

            RevokedAt = utcNow;
        }

        public void MarkAsUsed(DateTime utcNow)
        {
            LastUsedAt = utcNow;
        }
    }
}
