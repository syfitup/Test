using System;
using System.Collections.Generic;
using System.Text;

namespace SYF.Infrastructure.Configuration
{
    public class SystemOptions
    {
        public string Environment { get; set; }
        public string Region { get; set; }
        public string NotificationsBaseUrl { get; set; }
        public ConnectionStringOptions ConnectionStrings { get; set; }
        public SecurityOptions Security { get; set; }
    }
}
