
namespace FinanceCore.Application.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty ;
        public string PasswordHash { get; set; } = string.Empty;
        public string? TimeZone { get; set; }
        public int Role { get; set; }
        public int Attempts { get; set; } = 0;
        public DateTime? LockedUntil { get; set; } = null;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = null;
    }
}
