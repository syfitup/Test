using System;
using System.Collections.Generic;
using System.Text;

namespace SYF.Infrastructure.Entities
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
        bool Deleted { get; set; }
    }
}
