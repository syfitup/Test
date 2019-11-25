using System;
using System.Collections.Generic;
using System.Text;

namespace SYF.Infrastructure.Models
{
    public class UserApiModel
    {
        public Guid Id { get; set; }
        public byte[] ApiKey { get; set; }
    }
}
