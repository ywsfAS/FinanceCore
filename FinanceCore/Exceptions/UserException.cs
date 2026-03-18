using FinanceCore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Domain.Exceptions
{
    public class UserNotFoundException : DomainException
    {
        public Guid UserId { get; }

        public UserNotFoundException(Guid userId)
            : base($"User with ID '{userId}' was not found.")
        {
            UserId = userId;
        }
    }
    public class UserIdNotProvidedException : DomainException
    {
        public UserIdNotProvidedException()
            : base($"User Id Cannot be Empty")
        {
        }
    }
        // When user with email doesn't exist
        public class UserNotFoundByEmailException : DomainException
    {
        public string Email { get; }

        public UserNotFoundByEmailException(string email)
            : base($"User with email '{email}' was not found.")
        {
            Email = email;
        }
    }

    // When trying to operate on inactive user
    public class InactiveUserException : DomainException
    {
        public Guid UserId { get; }
        public string UserName { get; }

        public InactiveUserException(Guid userId, string userName)
            : base($"User '{userName}' (ID: {userId}) is inactive.")
        {
            UserId = userId;
            UserName = userName;
        }

        public InactiveUserException(Guid userId, string userName, string operation)
            : base($"Cannot {operation} for inactive user '{userName}' (ID: {userId}).")
        {
            UserId = userId;
            UserName = userName;
        }
    }

    // When user with email already exists
    public class DuplicateEmailException : DomainException
    {
        public string Email { get; }

        public DuplicateEmailException(string email)
            : base($"A user with email '{email}' already exists.")
        {
            Email = email;
        }
    }

    // When username is invalid
    public class InvalidUserNameException : DomainException
    {
        public string ProvidedName { get; }

        public InvalidUserNameException(string providedName, string reason)
            : base($"Invalid user name '{providedName}': {reason}")
        {
            ProvidedName = providedName;
        }
    }

    // When email is invalid (backup to Email value object validation)
    public class InvalidEmailException : DomainException
    {
        public string ProvidedEmail { get; }

        public InvalidEmailException(string providedEmail)
            : base($"Invalid email format: '{providedEmail}'")
        {
            ProvidedEmail = providedEmail;
        }

        public InvalidEmailException(string providedEmail, string reason)
            : base($"Invalid email '{providedEmail}': {reason}")
        {
            ProvidedEmail = providedEmail;
        }
    }

    // When password hash is missing or invalid
    public class InvalidPasswordHashException : DomainException
    {
        public InvalidPasswordHashException()
            : base("Password hash is required and cannot be empty.")
        {
        }

        public InvalidPasswordHashException(string reason)
            : base($"Invalid password hash: {reason}")
        {
        }
    }

    // When trying to change to same email
    public class EmailUnchangedException : DomainException
    {
        public string Email { get; }

        public EmailUnchangedException(string email)
            : base($"New email '{email}' is the same as current email.")
        {
            Email = email;
        }
    }

    // When trying to change to same currency
    public class CurrencyUnchangedException : DomainException
    {
        public EnCurrency Currency { get; }

        public CurrencyUnchangedException(EnCurrency currency)
            : base($"New currency '{currency}' is the same as current currency.")
        {
            Currency = currency;
        }
    }

    // When timezone is invalid
    public class InvalidTimeException : DomainException
    {
        public string TimeZone { get; }

        public InvalidTimeException(string timeZone)
            : base($"Invalid timezone: '{timeZone}'")
        {
            TimeZone = timeZone;
        }
    }

    // When user already activated
    public class UserAlreadyActivatedException : DomainException
    {
        public Guid UserId { get; }

        public UserAlreadyActivatedException(Guid userId)
            : base($"User {userId} is already activated.")
        {
            UserId = userId;
        }
    }

    // When user already deactivated
    public class UserAlreadyDeactivatedException : DomainException
    {
        public Guid UserId { get; }

        public UserAlreadyDeactivatedException(Guid userId)
            : base($"User {userId} is already deactivated.")
        {
            UserId = userId;
        }
    }

    // When user cannot be deleted (has dependencies)
    public class UserHasDependenciesException : DomainException
    {
        public Guid UserId { get; }
        public string Dependencies { get; }

        public UserHasDependenciesException(Guid userId, string dependencies)
            : base($"Cannot delete user {userId}. User has dependencies: {dependencies}")
        {
            UserId = userId;
            Dependencies = dependencies;
        }
    }

    // When email verification required
    public class EmailNotVerifiedException : DomainException
    {
        public Guid UserId { get; }
        public string Email { get; }

        public EmailNotVerifiedException(Guid userId, string email)
            : base($"Email '{email}' has not been verified for user {userId}.")
        {
            UserId = userId;
            Email = email;
        }
    }

    // When password is weak (if you add password strength validation)
    public class WeakPasswordException : DomainException
    {
        public List<string> Reasons { get; }

        public WeakPasswordException(List<string> reasons)
            : base($"Password does not meet requirements: {string.Join(", ", reasons)}")
        {
            Reasons = reasons;
        }
    }

    // When user session expired
    public class UserSessionExpiredException : DomainException
    {
        public Guid UserId { get; }

        public UserSessionExpiredException(Guid userId)
            : base($"Session for user {userId} has expired.")
        {
            UserId = userId;
        }
    }

    // When user authentication fails
    public class InvalidCredentialsException : DomainException
    {
        public InvalidCredentialsException()
            : base("Invalid email or password.")
        {
        }
    }

    // When user account is locked
    public class UserAccountLockedException : DomainException
    {
        public Guid UserId { get; }
        public DateTime? LockoutEnd { get; }

        public UserAccountLockedException(Guid userId, DateTime? lockoutEnd = null)
            : base(lockoutEnd.HasValue
                ? $"User account {userId} is locked until {lockoutEnd.Value:yyyy-MM-dd HH:mm:ss}."
                : $"User account {userId} is permanently locked.")
        {
            UserId = userId;
            LockoutEnd = lockoutEnd;
        }
    }
}
