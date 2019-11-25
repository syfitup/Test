using System;
using System.Collections.Generic;
using System.Text;

namespace SYF.Infrastructure.Models
{
    public class PersonRoleModel
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public bool Deleted { get; set; }
        public string RoleDescription { get; set; }
    }
}
