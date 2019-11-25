using System;
using System.Collections.Generic;
using System.Text;

namespace SYF.Infrastructure.Entities
{
    public class Entity<TKey> : IEntity<TKey>, IAuditEntity
    {
        public TKey Id { get; set; }
        public bool Deleted { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        public string ModifyUser { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}

