using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SYF.Infrastructure.Entities
{
    [Table("Departments", Schema = "dbo")]
    public class Department : Entity<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public IList<SubDepartment> Departments { get; set; } = new List<SubDepartment>();

    }
}
