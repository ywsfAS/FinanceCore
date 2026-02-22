using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Events.User;
using FinanceCore.Domain.Exceptions;

namespace FinanceCore.Domain.Users;

public class User : AggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public Email Email { get; private set; } = null!;
    public string? PasswordHash { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? LastLoginAt { get; private set; }

    // User preferences/settings
    public EnCurrency DefaultCurrency { get; private set; }
    public string? TimeZone { get; private set; }

    // Optional: Email verification
    public bool IsEmailVerified { get; private set; }
    public DateTime? EmailVerifiedAt { get; private set; }

    // Optional: Account lockout
    public int FailedLoginAttempts { get; private set; }
    public DateTime? LockoutEnd { get; private set; }



    private User() { }


    // Constructor For Db
    private User(Guid Id ,string name,
        Email email,
        string passwordHash,
        EnCurrency defaultCurrency = EnCurrency.USD,
        string? timeZone = null ) { 
        this.Id = Id;
        this.Name = name;
        this.Email = email;
        this.PasswordHash = passwordHash;
        this.DefaultCurrency = defaultCurrency;
        this.TimeZone = TimeZone;
        this.IsActive = true;
    }
    public static User Create(Guid Id,string name,
        Email email,
        string passwordHash,
        EnCurrency defaultCurrency = EnCurrency.USD,
        string? timeZone = null)
    {
        return new User(Id, name, email, passwordHash, defaultCurrency, timeZone);
    }
    public static User Create(
        string name,
        Email email,
        string passwordHash,
        EnCurrency defaultCurrency = EnCurrency.USD,
        string? timeZone = null)
    {
        // Validate name
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidUserNameException(name, "Name cannot be empty");

        if (name.Length > 100)
            throw new InvalidUserNameException(name, "Name cannot exceed 100 characters");

        if (name.Length < 2)
            throw new InvalidUserNameException(name, "Name must be at least 2 characters");

        // Validate email (Email value object should handle this, but double-check)
        if (email == null)
            throw new ArgumentNullException(nameof(email));

        // Validate password hash
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new InvalidPasswordHashException();

        // Validate timezone if provided
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
            IsActive = true,
            IsEmailVerified = false,
            FailedLoginAttempts = 0,
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
        // Check if user is active
        if (!IsActive)
            throw new InactiveUserException(Id, Name, "update profile");

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
                throw new InvalidTimeException(timeZone);

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
        // Check if user is active
        if (!IsActive)
            throw new InactiveUserException(Id, Name, "change email");

        if (newEmail == null)
            throw new ArgumentNullException(nameof(newEmail));

        if (newEmail.Address == Email.Address)
            throw new EmailUnchangedException(newEmail.Address);

        var oldEmail = Email;
        Email = newEmail;
        IsEmailVerified = false; // Require re-verification
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new UserEmailChangedEvent(
            Id,
            oldEmail.Address,
            newEmail.Address));
    }

    public void ChangePassword(string newPasswordHash, string? oldPasswordHash = null)
    {
        // Check if user is active
        if (!IsActive)
            throw new InactiveUserException(Id, Name, "change password");

        if (string.IsNullOrWhiteSpace(newPasswordHash))
            throw new InvalidPasswordHashException();

        // Optional: Verify old password matches (if provided)
        if (oldPasswordHash != null && oldPasswordHash != PasswordHash)
            throw new InvalidCredentialsException();

        PasswordHash = newPasswordHash;
        UpdatedAt = DateTime.UtcNow;

        // Reset failed login attempts on password change
        FailedLoginAttempts = 0;
        LockoutEnd = null;

        AddDomainEvent(new UserPasswordChangedEvent(Id));
    }

    public void ChangeDefaultCurrency(EnCurrency newCurrency)
    {
        // Check if user is active
        if (!IsActive)
            throw new InactiveUserException(Id, Name, "change default currency");

        if (newCurrency == DefaultCurrency)
            throw new CurrencyUnchangedException(newCurrency);

        var oldCurrency = DefaultCurrency;
        DefaultCurrency = newCurrency;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new UserDefaultCurrencyChangedEvent(
            Id,
            oldCurrency,
            newCurrency));
    }

    public void RecordLogin()
    {
        if (!IsActive)
            throw new InactiveUserException(Id, Name, "login");

        // Check if account is locked
        if (IsLocked())
            throw new UserAccountLockedException(Id, LockoutEnd);

        LastLoginAt = DateTime.UtcNow;
        FailedLoginAttempts = 0; // Reset on successful login
        LockoutEnd = null;

        AddDomainEvent(new UserLoggedInEvent(Id, LastLoginAt.Value));
    }

    public void RecordFailedLogin()
    {
        FailedLoginAttempts++;

        // Lock account after 5 failed attempts
        if (FailedLoginAttempts >= 5)
        {
            LockoutEnd = DateTime.UtcNow.AddMinutes(30); // Lock for 30 minutes
            AddDomainEvent(new UserAccountLockedEvent(Id, LockoutEnd.Value));
        }

        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        if (!IsActive)
            throw new UserAlreadyDeactivatedException(Id);

        IsActive = false;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new UserDeactivatedEvent(Id, Name));
    }

    public void Activate()
    {
        if (IsActive)
            throw new UserAlreadyActivatedException(Id);

        IsActive = true;
        FailedLoginAttempts = 0;
        LockoutEnd = null;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new UserActivatedEvent(Id, Name));
    }

    public void VerifyEmail()
    {
        if (IsEmailVerified)
            return;

        IsEmailVerified = true;
        EmailVerifiedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new UserEmailVerifiedEvent(Id, Email.Address));
    }

    public void UnlockAccount()
    {
        if (!IsLocked())
            return;

        FailedLoginAttempts = 0;
        LockoutEnd = null;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new UserAccountUnlockedEvent(Id));
    }

    // Query methods
    public bool IsLocked()
    {
        return LockoutEnd.HasValue && LockoutEnd.Value > DateTime.UtcNow;
    }

    // Private helper
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