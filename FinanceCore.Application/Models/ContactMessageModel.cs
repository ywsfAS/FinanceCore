
namespace FinanceCore.Application.Models
{
    public class ContactMessageModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; }
        public byte Subject { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool IsProccessed { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
