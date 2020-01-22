using System;
using System.Collections.Generic;
using System.Text;

namespace SYF.Infrastructure.Models.Requests
{
    public class SubDepartmentSearchRequest
    {
        public string Term { get; set; }
        public Guid? DepartmentId { get; set; }
    }
}
