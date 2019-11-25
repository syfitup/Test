using SYF.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SYF.Infrastructure.Exceptions
{
    public class AuthenticateException : Exception
    {
        public AuthenticateException()
            : base("Authentication failed")
        {
        }

        public AuthenticateException(string message)
            : base(message)
        {
        }

        public string UserName { get; set; }
        public AuthenticateFailureReason Reason { get; set; }
    }
}
