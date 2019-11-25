using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SYF.Infrastructure.Entities
{
    [Table("PersonPositions", Schema = "dbo")]
    public class PersonPosition : Entity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? ParentId { get; set; }
        public Guid SiteId { get; set; }

        public PersonPosition Parent { get; set; }
        public ICollection<Person> People { get; set; } = new List<Person>();
    }
}
