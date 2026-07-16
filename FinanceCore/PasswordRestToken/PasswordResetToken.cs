using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.PasswordRestToken
{
    public class PasswordResetToken : Entity
    {
        public Guid UserId { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public bool IsUsed { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public PasswordResetToken(Guid userId , string token , DateTime expiresAt) {
            UserId = userId;
            Token = token;
            ExpiresAt = expiresAt; 
        }
        private PasswordResetToken() { }
        public void MarkAsUsed()
        {
            IsUsed = true;
        }
        public bool IsExpired()
        {
            return ExpiresAt <= DateTime.UtcNow;
        }
    }
}
