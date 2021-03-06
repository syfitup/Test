﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SYF.Infrastructure.Entities
{
    [Table("SubDepartments", Schema = "dbo")]
    public class SubDepartment : Entity<Guid>
    {
        public Guid DepartmentId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public Department Department { get; set; }


    }
}
