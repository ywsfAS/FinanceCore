using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;

namespace FinanceCore.Domain.ContactMessage
{
    // Represents a contact message from user
    public class ContactMessage : Entity
    {
        public string FullName { get; private set; } = string.Empty;
        public Email Email { get; private set; }
        public EnMessageSubject Subject { get; private set; } = EnMessageSubject.TechnicalSupport;
        public string Message { get; private set; } = string.Empty;
        public bool IsProccessed { get; private set; } = false;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        private ContactMessage() { }// Dapper private constructor
        public ContactMessage(string fullName, Email email, EnMessageSubject subject, string message, DateTime createdAt)
        {
            FullName = fullName;
            Email = email;
            Subject = subject;
            Message = message;
            CreatedAt = createdAt;
        }
        public static ContactMessage Load(Guid id , string fullName, Email email, EnMessageSubject subject, string message, DateTime createdAt) {
            return new ContactMessage
            {
                Id = id,
                FullName = fullName,
                Email = email,
                Subject = subject,
                Message = message,
                CreatedAt = createdAt
            }; 
        }
        public void MarkAsProccessed()
        {
            IsProccessed = true;
        }
    }
}
