using System;
using System.Collections.Generic;
using System.Text;

namespace SYF.Infrastructure.Models.Requests
{
    public class TimesheetSearchRequest
    {
        public Guid? PersonId { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? SubDepartmentId { get; set; }
        public DateTime FromDate { get; set; }
        public int? Periods { get; set; }
    }
}
