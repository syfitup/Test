using SYF.Infrastructure.Enums;
using System;
using System.Collections.Generic;

namespace SYF.Infrastructure.Models
{
    public class EntityChange
    {
        public EntityChange()
        {
            Changes = new List<PropertyChange>();
        }

        public object Entity { get; set; }
        public Type EntityType { get; set; }
        public EventType EventType { get; set; }
        public IList<PropertyChange> Changes { get; set; }
    }
}
