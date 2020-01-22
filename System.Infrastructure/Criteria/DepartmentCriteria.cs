using SYF.Infrastructure.Data;
using SYF.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SYF.Infrastructure.Criteria
{
    public class SubDepartmentCriteria : BaseCriteria<SubDepartment>
    {
        public Guid? Id { get; set; }
        public Guid?[] Ids { get; set; }
        public string Name { get; set; }
        public string Term { get; set; }
        public bool? Deleted { get; set; }

        public override IQueryable<SubDepartment> Apply(IQueryable<SubDepartment> qry)
        {
            if (Ids?.Length > 0)
                qry = qry.Where(x => Ids.Contains(x.Id));

            if (Id != null)
                qry = qry.Where(x => x.Id == Id);

            if (!string.IsNullOrEmpty(Name))
                qry = qry.Where(a => a.Name == Name);

            if (!string.IsNullOrEmpty(Term))
                qry = qry.Where(a => a.Code.Contains(Term) || a.Name.Contains(Term));

            if (Deleted != null)
                qry = qry.Where(x => x.Deleted == Deleted);

            return qry;
        }
    }
}
