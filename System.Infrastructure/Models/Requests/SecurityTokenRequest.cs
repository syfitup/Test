using System;
using System.Collections.Generic;
using System.Text;

namespace SYF.Infrastructure.Models.Requests
{
    public class SecurityTokenRequest
    {
        public Guid? Id { get; set; }
        public Guid? PersonId { get; set; }
        public Guid? SiteId { get; set; }
        public string Name { get; set; }
        public bool SuperUser { get; set; }
        public IEnumerable<Guid> Roles { get; set; }
        public int? Duration { get; set; }
    }
}
