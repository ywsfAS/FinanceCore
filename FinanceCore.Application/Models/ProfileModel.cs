using FinanceCore.Domain.Enums;

namespace FinanceCore.Application.Models
{
    public class ProfileModel
    {
        public Guid UserId { get; set; }
        public string FirstName { get;  set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public string? AvatarUrl { get;  set; }
        public EnCurrency Currency { get; set; }
    }
}
