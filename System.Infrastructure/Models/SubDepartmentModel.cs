using System;
using System.Collections.Generic;
using System.Text;

namespace SYF.Infrastructure.Models
{
    public class SubDepartmentModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; }

    }
}
