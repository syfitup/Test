using System;

namespace SYF.Infrastructure.Models
{
    public class UserSearchRequest
    {
        public string Term { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? SubDepartmentId { get; set; }
        public Guid? PersonPositionId { get; set; }
        public Guid? RoleId { get; set; }
    }
}
