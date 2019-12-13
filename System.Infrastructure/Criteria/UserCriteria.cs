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
        public Guid? DepartmentId { get; set; }
        public Guid? SubDepartmentId { get; set; }
        public Guid? PersonPositionId { get; set; }
        public Guid? RoleId { get; set; }
        public bool? Deleted { get; set; }

        public override IQueryable<User> Apply(IQueryable<User> qry)
        {
            if (Id != null)
                qry = qry.Where(x => x.Id == Id);

            if (!string.IsNullOrEmpty(UserName))
                qry = qry.Where(x => x.UserName == UserName);

            if (!string.IsNullOrEmpty(Term))
                qry = qry.Where(x => x.UserName.Contains(Term) || x.Person.Name.Contains(Term) || x.Person.EmailAddress.Contains(Term));

            if (DepartmentId != null)
                qry = qry.Where(x => x.Person.DepartmentId == DepartmentId);

            if (SubDepartmentId != null)
                qry = qry.Where(x => x.Person.SubDepartmentId == SubDepartmentId);
            
            if (PersonPositionId != null)
                qry = qry.Where(x => x.Person.PersonPositionId == PersonPositionId);

            if (Deleted != null)
                qry = qry.Where(x => x.Deleted == Deleted);

            return qry;
        }
    }
}