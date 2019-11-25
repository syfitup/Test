using System;

namespace SYF.Infrastructure.Models
{
    public class TenantModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ConnectionString { get; set; }
        public string Theme { get; set; }
    }
}
