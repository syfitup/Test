using System;
using System.Collections.Generic;
using System.Text;

namespace SYF.Infrastructure.Models.Requests
{
    public class AuthenticateRequest
    {
        public string DomainName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool SuperUser { get; set; }
    }
}
