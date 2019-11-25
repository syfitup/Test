using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SYF.Infrastructure.Entities
{

    [Table("PersonRoles", Schema = "dbo")]
    public class PersonRole : Entity<Guid>
    {
        public Guid PersonId { get; set; }
        public Guid RoleId { get; set; }

        public Person Person { get; set; }
        public Role Role { get; set; }
    }
}
