using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Domain.Exceptions
{
    public class ProfileException
    {
        public class ProfileAlreadyExistsException : DomainException
        {
            public Guid id { get; }

            public ProfileAlreadyExistsException(Guid profileId)
                : base($"Profile With id : ${profileId} Already Exists")
            {
                id = profileId;
            }
        }
        public class ProfileNotFoundException : DomainException
        {
            public Guid id { get; }

            public ProfileNotFoundException(Guid profileId)
                : base($"Profile With id : ${profileId} Not Found")
            {
                id = profileId;
            }
        }

        public class InvalidFirstNameException : DomainException
        {
            public string ProvidedFirstName { get; }

            public InvalidFirstNameException(string providedFirstName, string reason)
                : base($"Invalid first name '{providedFirstName}': {reason}")
            {
                ProvidedFirstName = providedFirstName;
            }
        }
        public class InvalidLastNameException : DomainException
        {
            public string ProvidedLastName { get; }

            public InvalidLastNameException(string providedLastName, string reason)
                : base($"Invalid last name '{providedLastName}': {reason}")
            {
                ProvidedLastName = providedLastName;
            }
        }
        public class InvalidAvatarException : DomainException
        {
            public string ProvidedAvatar { get; }

            public InvalidAvatarException(string providedAvatar, string reason)
                : base($"Invalid avatar '{providedAvatar}': {reason}")
            {
                ProvidedAvatar = providedAvatar;
            }
        }
        public class UnsupportedCurrencyException : DomainException
        {
            public string ProvidedCurrency { get; }

            public UnsupportedCurrencyException(string providedCurrency)
                : base($"Unsupported currency '{providedCurrency}'")
            {
                ProvidedCurrency = providedCurrency;
            }
        }
    }
}
