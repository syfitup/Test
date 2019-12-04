using System;
using System.Linq;
using SYF.Infrastructure.Data;
using SYF.Infrastructure.Entities;

namespace SYF.Infrastructure.Criteria
{
    public class UserCriteria : BaseCriteria<User>
    {
        public Guid? Id { get; set; }
        public string UserName { get; set; }
        public string Term { get; set; }
        public Guid? BusinessUnitId { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? TeamId { get; set; }
        public Guid? PersonPositionId { get; set; }
        public Guid? RoleId { get; set; }
        public Guid? SiteId { get; set; }
        public bool? Deleted { get; set; }

        public override IQueryable<User> Apply(IQueryable<User> qry)
        {
            if (Id != null)
                qry = qry.Where(x => x.Id == Id);

            if (!string.IsNullOrEmpty(UserName))
                qry = qry.Where(x => x.UserName == UserName);

            if (!string.IsNullOrEmpty(Term))
                qry = qry.Where(x => x.UserName.Contains(Term) || x.Person.Name.Contains(Term) || x.Person.EmailAddress.Contains(Term));

            if (BusinessUnitId != null)
                qry = qry.Where(x => x.Person.DepartmentId == BusinessUnitId);

            if (DepartmentId != null)
                qry = qry.Where(x => x.Person.SubDepartmentId == DepartmentId);
            
            if (PersonPositionId != null)
                qry = qry.Where(x => x.Person.PersonPositionId == PersonPositionId);

            if (Deleted != null)
                qry = qry.Where(x => x.Deleted == Deleted);

            return qry;
        }
    }
}