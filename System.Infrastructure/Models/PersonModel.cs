using System;
using System.Collections.Generic;
using System.Text;

namespace SYF.Infrastructure.Models
{
    public class PersonModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public Guid? BusinessUnitId { get; set; }
        public string BusinessUnitName { get; set; }
        public Guid? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public Guid? PersonPositionId { get; set; }
        public string PersonPositionName { get; set; }
        public DateTime? RarEndDate { get; set; }
        public Guid? SiteId { get; set; }
        public int? RarFrequency { get; set; }
        public List<PersonAccessModel> Access { get; set; }
    }
}
