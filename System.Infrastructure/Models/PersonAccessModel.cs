using System;
using System.Collections.Generic;
using System.Text;

namespace SYF.Infrastructure.Models
{
    public class PersonAccessModel
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public Guid? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public Guid? SubDepartmentId { get; set; }
        public string SubDepartmentName { get; set; }
        public bool Deleted { get; set; }
    }
}
