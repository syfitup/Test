using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SYF.Infrastructure.Entities
{
    [Table("PersonAccess", Schema = "dbo")]
    public class PersonAccess : Entity<Guid>
    {
        public Guid PersonId { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? SubDepartmentId { get; set; }

        public Department Department { get; set; }
        public SubDepartment SubDepartment { get; set; }
    }
}

