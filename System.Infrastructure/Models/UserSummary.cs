using System;

namespace SYF.Infrastructure.Models
{
    public class UserSummary
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string GroupName { get; set; }
        public string DepartmentName { get; set; }
        public string SubDepartmentName { get; set; }
        public string PersonPositionName { get; set; }
        public string EmailAddress { get; set; }
        public bool LockedOut { get; set; }
        public bool Disabled { get; set; }
        public Guid PersonId { get; set; }
    }

}
