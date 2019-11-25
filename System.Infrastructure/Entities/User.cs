using SYF.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SYF.Infrastructure.Entities
{
    public class User : Entity<Guid>
    {
        public Guid PersonId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public DateTime? PasswordModifyDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? LockoutDate { get; set; }
        public DateTime? FailedPasswordDate { get; set; }
        public int FailedPasswordCount { get; set; }
        public string Token { get; set; }
        public DateTime? TokenDate { get; set; }
        public byte[] ApiKey { get; set; }
        public UserFlags Flags { get; set; }
        public Guid SiteId { get; set; }

        public Person Person { get; set; }
    }
}
