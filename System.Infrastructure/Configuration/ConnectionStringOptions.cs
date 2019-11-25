using System;
using System.Collections.Generic;
using System.Text;

namespace SYF.Infrastructure.Configuration
{
    public class ConnectionStringOptions
    {
        public string Tenants { get; set; }
        public string Files { get; set; }
        public string Queues { get; set; }
        public string Cache { get; set; }
        public string Hangfire { get; set; }
    }
}
