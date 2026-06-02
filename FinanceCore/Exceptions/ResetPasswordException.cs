using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Domain.Exceptions
{
    public class ResetPasswordException
    {
        public class InvalidTokenException : DomainException
        {
            public Guid id { get; }

            public InvalidTokenException()
                : base("Invalid token")
            {
               
            }
        }
        public class UsedTokenException : DomainException
        {
            public Guid id { get; }

            public UsedTokenException()
                : base("Token is already used")
            {

            }
        }
    }
}
