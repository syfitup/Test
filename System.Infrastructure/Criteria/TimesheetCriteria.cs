using SYF.Infrastructure.Data;
using SYF.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SYF.Infrastructure.Criteria
{
    public class TimesheetCriteria : BaseCriteria<Timesheet>
    {
        public Guid? PersonId { get; set; }
        public string PersonName { get; set; }
        public DateTime TimesheetDate { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool? Deleted { get; set; }

        public override IQueryable<Timesheet> Apply(IQueryable<Timesheet> qry)
        {
            if (PersonId != null)
                qry = qry.Where(a => a.PersonId == PersonId);

            if (!string.IsNullOrEmpty(PersonName))
                qry = qry.Where(a => a.PersonName.Trim() == PersonName.Trim());

            if (TimesheetDate != null)
                qry = qry.Where(a => a.TimesheetDate == TimesheetDate);

            if (FromDate != null)
                qry = qry.Where(a => a.TimesheetDate >= FromDate);

            if (ToDate != null)
                qry = qry.Where(a => a.TimesheetDate <= ToDate);

            if (Deleted != null)
                qry = qry.Where(a => a.Deleted == Deleted);

            return qry;
        }
    }
}
