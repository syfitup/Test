using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SYF.Infrastructure.Entities
{
    [Table("People", Schema = "dbo")]
    public class Person : Entity<Guid>
    {
        public Person()
        {
            Roles = new List<PersonRole>();
            Access = new List<PersonAccess>();
        }

        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public Guid? PersonPositionId { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? SubDepartmentId { get; set; }

        public PersonPosition PersonPosition { get; set; }
        public List<PersonRole> Roles { get; set; }
        public SubDepartment SubDepartment { get; set; }
        public Department Department { get; set; }
        public List<PersonAccess> Access
        {
            get; set;
        }
    }
}
