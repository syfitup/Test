using System;

namespace SYF.Infrastructure.Entities
{
    public interface IAuditEntity
    {
        string CreateUser { get; set; }
        DateTime CreateDate { get; set; }
        string ModifyUser { get; set; }
        DateTime ModifyDate { get; set; }
    }
}
