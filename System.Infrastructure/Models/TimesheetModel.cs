using System;
using System.Collections.Generic;
using System.Text;

namespace SYF.Infrastructure.Models
{
    public class TimesheetModel
    {
        public Guid Id { get; set; }
        public Guid? PersonId { get; set; }
        public string PersonName { get; set; }
        public DateTime TimesheetDate { get; set; }
        public decimal? TimesheetEmployeeHours { get; set; }
    }
}
