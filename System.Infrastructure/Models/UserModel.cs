using System;
using System.Collections.Generic;
using System.Text;

namespace SYF.Infrastructure.Models
{
    public class UserModel
    {
        public UserModel()
        {
            Roles = new List<PersonRoleModel>();
            Access = new List<PersonAccessModel>();
        }

        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid PersonId { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? SubDepartmentId { get; set; }
        public bool Disabled { get; set; }

        public List<PersonRoleModel> Roles { get; set; }
        public List<PersonAccessModel> Access { get; set; }
        
    }
}
