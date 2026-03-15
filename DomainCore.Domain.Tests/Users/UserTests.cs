using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Exceptions;
using FinanceCore.Domain.Users;
using FluentAssertions;
using System;
using Xunit;

namespace FinanceCore.Domain.Tests.Users
{
    public class UserTests
    {
        [Fact]
        public void CreateUser_ShouldInitializeProperly()
        {
            // Arrange
            var name = "John Doe";
            var email = new Email("john@example.com");
            var passwordHash = "hashed_password";
            var defaultCurrency = EnCurrency.USD;
            var timeZone = "UTC";

            // Act
            var user = User.Create(name, email, passwordHash, defaultCurrency, timeZone);

            // Assert
            user.Name.Should().Be(name);
            user.Email.Address.Should().Be(email.Address);
            user.PasswordHash.Should().Be(passwordHash);
            user.DefaultCurrency.Should().Be(defaultCurrency);
            user.TimeZone.Should().Be(timeZone);
            user.UpdatedAt.Should().BeNull();
        }

        [Fact]
        public void CreateUser_WithInvalidName_ShouldThrow()
        {
            var email = new Email("test@example.com");
            Action actEmpty = () => User.Create("", email, "hash");
            Action actShort = () => User.Create("A", email, "hash");
            Action actLong = () => User.Create(new string('x', 101), email, "hash");

            actEmpty.Should().Throw<InvalidUserNameException>();
            actShort.Should().Throw<InvalidUserNameException>();
            actLong.Should().Throw<InvalidUserNameException>();
        }

        [Fact]
        public void CreateUser_WithInvalidPassword_ShouldThrow()
        {
            var email = new Email("test@example.com");
            Action actNull = () => User.Create("John", email, null!);
            Action actEmpty = () => User.Create("John", email, "");

            actNull.Should().Throw<InvalidPasswordHashException>();
            actEmpty.Should().Throw<InvalidPasswordHashException>();
        }

        [Fact]
        public void CreateUser_WithInvalidTimeZone_ShouldThrow()
        {
            var email = new Email("john@example.com");
            Action act = () => User.Create("John", email, "hash", EnCurrency.USD, "InvalidTZ");
            act.Should().Throw<InvalidTimeZoneException>();
        }

        [Fact]
        public void UpdateProfile_ShouldUpdateNameAndTimeZone()
        {
            var user = User.Create("John", new Email("john@example.com"), "hash");

            user.UpdateProfile("Jane Doe", "UTC");

            user.Name.Should().Be("Jane Doe");
            user.TimeZone.Should().Be("UTC");
            user.UpdatedAt.Should().NotBeNull();
        }

        [Fact]
        public void UpdateProfile_InvalidName_ShouldThrow()
        {
            var user = User.Create("John", new Email("john@example.com"), "hash");

            Action actEmpty = () => user.UpdateProfile("");
            Action actShort = () => user.UpdateProfile("A");
            Action actLong = () => user.UpdateProfile(new string('x', 101));

            actEmpty.Should().Throw<InvalidUserNameException>();
            actShort.Should().Throw<InvalidUserNameException>();
            actLong.Should().Throw<InvalidUserNameException>();
        }

        [Fact]
        public void UpdateProfile_InvalidTimeZone_ShouldThrow()
        {
            var user = User.Create("John", new Email("john@example.com"), "hash");

            Action act = () => user.UpdateProfile(timeZone: "InvalidTZ");
            act.Should().Throw<InvalidTimeZoneException>();
        }

        [Fact]
        public void ChangeEmail_ShouldUpdateEmail()
        {
            var user = User.Create("John", new Email("john@example.com"), "hash");
            var newEmail = new Email("jane@example.com");

            user.ChangeEmail(newEmail);

            user.Email.Address.Should().Be("jane@example.com");
            user.UpdatedAt.Should().NotBeNull();
        }

        [Fact]
        public void ChangeEmail_SameEmail_ShouldThrow()
        {
            var user = User.Create("John", new Email("john@example.com"), "hash");
            var sameEmail = new Email("john@example.com");

            Action act = () => user.ChangeEmail(sameEmail);
            act.Should().Throw<EmailUnchangedException>();
        }

        [Fact]
        public void ChangePassword_ShouldUpdatePassword()
        {
            var user = User.Create("John", new Email("john@example.com"), "hash");

            user.ChangePassword("newhash");

            user.PasswordHash.Should().Be("newhash");
            user.UpdatedAt.Should().NotBeNull();
        }

        [Fact]
        public void ChangePassword_InvalidOldPassword_ShouldThrow()
        {
            var user = User.Create("John", new Email("john@example.com"), "oldhash");

            Action act = () => user.ChangePassword("newhash", "wrongold");
            act.Should().Throw<InvalidCredentialsException>();
        }

        [Fact]
        public void ChangeDefaultCurrency_ShouldUpdateCurrency()
        {
            var user = User.Create("John", new Email("john@example.com"), "hash", EnCurrency.USD);

            user.ChangeDefaultCurrency(EnCurrency.EUR);

            user.DefaultCurrency.Should().Be(EnCurrency.EUR);
            user.UpdatedAt.Should().NotBeNull();
        }

        [Fact]
        public void ChangeDefaultCurrency_SameCurrency_ShouldThrow()
        {
            var user = User.Create("John", new Email("john@example.com"), "hash", EnCurrency.USD);

            Action act = () => user.ChangeDefaultCurrency(EnCurrency.USD);
            act.Should().Throw<CurrencyUnchangedException>();
        }
    }
}
