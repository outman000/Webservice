using System;
using System.Collections.Generic;
using System.Text;

namespace User.Domain.Exceptions
{
    public class UserDomainException : Exception
    {
        public UserDomainException()
        { }

        public UserDomainException(string message)
            : base(message)
        { }

        public UserDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
