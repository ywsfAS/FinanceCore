using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Events.User;
using FinanceCore.Domain.Exceptions;

namespace FinanceCore.Domain.Users
{
    public class User : AggregateRoot
    {
        public string Name { get; private set; } = string.Empty;
        public Email Email { get; private set; } = null!;
        public string? PasswordHash { get; private set; }
        public EnCurrency DefaultCurrency { get; private set; }
        public string? TimeZone { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        private User() { }

        private User(
            Guid id,
            string name,
            Email email,
            string passwordHash,
            EnCurrency defaultCurrency,
            string? timeZone,
            DateTime createdAt,
            DateTime? updatedAt)
        {
            Id = id;
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            DefaultCurrency = defaultCurrency;
            TimeZone = timeZone;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        // Reconstitute from persistence
        public static User Create(
            Guid id,
            string name,
            Email email,
            string passwordHash,
            EnCurrency defaultCurrency,
            string? timeZone,
            DateTime createdAt,
            DateTime? updatedAt = null)
        {
            return new User(id, name, email, passwordHash, defaultCurrency, timeZone, createdAt, updatedAt);
        }

        // Create new user
        public static User Create(
            string name,
            Email email,
            string passwordHash,
            EnCurrency defaultCurrency = EnCurrency.USD,
            string? timeZone = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidUserNameException(name, "Name cannot be empty");

            if (name.Length > 100)
                throw new InvalidUserNameException(name, "Name cannot exceed 100 characters");

            if (name.Length < 2)
                throw new InvalidUserNameException(name, "Name must be at least 2 characters");

            if (email == null)
                throw new ArgumentNullException(nameof(email));

            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new InvalidPasswordHashException();

            if (timeZone != null && !IsValidTimeZone(timeZone))
                throw new InvalidTimeZoneException(timeZone);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = name.Trim(),
                Email = email,
                PasswordHash = passwordHash,
                DefaultCurrency = defaultCurrency,
                TimeZone = timeZone,
                CreatedAt = DateTime.UtcNow
            };

            user.AddDomainEvent(new UserCreatedEvent(
                user.Id,
                user.Name,
                user.Email.Address));

            return user;
        }

        public void UpdateProfile(string? name = null, string? timeZone = null)
        {
            var hasChanges = false;

            if (name != null && name != Name)
            {
                if (string.IsNullOrWhiteSpace(name))
                    throw new InvalidUserNameException(name, "Name cannot be empty");

                if (name.Length > 100)
                    throw new InvalidUserNameException(name, "Name cannot exceed 100 characters");

                if (name.Length < 2)
                    throw new InvalidUserNameException(name, "Name must be at least 2 characters");

                Name = name.Trim();
                hasChanges = true;
            }

            if (timeZone != null && timeZone != TimeZone)
            {
                if (!IsValidTimeZone(timeZone))
                    throw new InvalidTimeZoneException(timeZone);

                TimeZone = timeZone;
                hasChanges = true;
            }

            if (hasChanges)
            {
                UpdatedAt = DateTime.UtcNow;
                AddDomainEvent(new UserProfileUpdatedEvent(Id, Name));
            }
        }

        public void ChangeEmail(Email newEmail)
        {
            if (newEmail == null)
                throw new ArgumentNullException(nameof(newEmail));

            if (newEmail.Address == Email.Address)
                throw new EmailUnchangedException(newEmail.Address);

            var oldEmail = Email;
            Email = newEmail;
            UpdatedAt = DateTime.UtcNow;

            AddDomainEvent(new UserEmailChangedEvent(Id, oldEmail.Address, newEmail.Address));
        }

        public void ChangePassword(string newPasswordHash, string? oldPasswordHash = null)
        {
            if (string.IsNullOrWhiteSpace(newPasswordHash))
                throw new InvalidPasswordHashException();

            if (oldPasswordHash != null && oldPasswordHash != PasswordHash)
                throw new InvalidCredentialsException();

            PasswordHash = newPasswordHash;
            UpdatedAt = DateTime.UtcNow;

            AddDomainEvent(new UserPasswordChangedEvent(Id));
        }

        public void ChangeDefaultCurrency(EnCurrency newCurrency)
        {
            if (newCurrency == DefaultCurrency)
                throw new CurrencyUnchangedException(newCurrency);

            var oldCurrency = DefaultCurrency;
            DefaultCurrency = newCurrency;
            UpdatedAt = DateTime.UtcNow;

            AddDomainEvent(new UserDefaultCurrencyChangedEvent(Id, oldCurrency, newCurrency));
        }

        private static bool IsValidTimeZone(string timeZone)
        {
            try
            {
                TimeZoneInfo.FindSystemTimeZoneById(timeZone);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

}
