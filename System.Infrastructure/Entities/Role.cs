using System;
using System.Collections.Generic;
using System.Text;

namespace SYF.Infrastructure.Entities
{
    public class Role : Entity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Permissions { get; set; }

        public List<PersonRole> People { get; set; }
    }
}
