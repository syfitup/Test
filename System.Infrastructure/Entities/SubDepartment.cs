using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SYF.Infrastructure.Entities
{
    [Table("SubDepartments", Schema = "dbo")]
    public class SubDepartment : Entity<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public IList<Department> Departments { get; set; } = new List<Department>();


    }
}
