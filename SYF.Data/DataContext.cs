using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SYF.Infrastructure.Entities;
using SYF.Infrastructure.Providers;

namespace SYF.Data
{
    public class DataContext : BaseDataContext
    {
        public DataContext(DbContextOptions<DataContext> options, IClaimsPrincipalProvider principalProvider, ILoggerFactory loggerFactory)
            : base(options, principalProvider, loggerFactory)
        
        {
        }

        //dbo

        public DbSet<Person> People { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PersonAccess> PersonAccess { get; set; }
        public DbSet<PersonRole> PersonRoles { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<SubDepartment> SubDepartments { get; set; }



    }

    public class TransientDataContext : DataContext
    {
        public TransientDataContext(DbContextOptions<DataContext> options, IClaimsPrincipalProvider principalProvider, ILoggerFactory loggerFactory)
            : base(options, principalProvider, loggerFactory) { }
    }
}

