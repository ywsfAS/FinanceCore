using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Events.Profile;
using FinanceCore.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FinanceCore.Domain.Exceptions.ProfileException;

namespace FinanceCore.Domain.Profile
{
    public class Profile : AggregateRoot
    {
        public Guid UserId { get; private set; }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Bio { get; private set; }
        public string? AvatarUrl { get; private set; }
        public EnCurrency Currency { get; private set; }


        public static Profile Create(Guid userId, string firstName, string lastName,string bio , string avatarUrl , EnCurrency currency)
        {
            if (userId == Guid.Empty)
                throw new UserIdNotProvidedException();

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
                throw new InvalidFirstNameException(firstName + lastName,"Invalid name");

            var profile = new Profile
            {
                UserId = userId,
                FirstName = firstName,
                LastName = lastName,
                Bio = bio,
                AvatarUrl = avatarUrl,
                Currency = EnCurrency.USD
            };

            profile.AddDomainEvent(new ProfileCreatedEvent(userId));

            return profile;
        }

        public void UpdateName(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
                throw new InvalidFirstNameException(firstName + lastName, "Invalid name");

            FirstName = firstName;
            LastName = lastName;

            AddDomainEvent(new ProfileNameUpdatedEvent(UserId, firstName, lastName));
        }

        public void UpdateAvatar(string avatarUrl)
        {
            if (string.IsNullOrWhiteSpace(avatarUrl))
                throw new InvalidAvatarException(avatarUrl,"Invalid avatar");

            AvatarUrl = avatarUrl;

            AddDomainEvent(new ProfileAvatarUpdatedEvent(UserId, avatarUrl));
        }

        public void ChangeCurrency(EnCurrency currency)
        {

            if (Enum.IsDefined(currency))
                throw new UnsupportedCurrencyException(currency.ToString());

            if (Currency == currency)
                return; // avoid useless event

            Currency = currency;

            AddDomainEvent(new ProfileCurrencyChangedEvent(UserId, currency));
        }
    }
}
