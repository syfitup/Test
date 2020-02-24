using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SYF.Infrastructure.Entities
{
    [Table("Timesheets", Schema = "dbo")]
    public class Timesheet : Entity<Guid>
    {
        public Guid? PersonId { get; set; }
        public DateTime TimesheetDate { get; set; }
        public decimal? TimesheetEmployeeHours { get; set; }
       
        public Person Person { get; set; }
    }
}
