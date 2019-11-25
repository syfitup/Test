using System;
using System.Collections.Generic;
using System.Text;

namespace SYF.Infrastructure.Configuration
{
    public class SecurityOptions
    {
        public string SecretKey { get; set; }
        public string Algorithm { get; set; }
        public string Issuer { get; set; }
        public int Duration { get; set; }
        public string SuperUserPassword { get; set; }
        public string SuperUserPasswordSalt { get; set; }
    }
}
