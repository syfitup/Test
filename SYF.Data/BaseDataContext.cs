using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using SYF.Infrastructure.Entities;
using SYF.Infrastructure.Enums;
using SYF.Infrastructure.Models;
using SYF.Infrastructure.Providers;

namespace SYF.Data
{
    public class BaseDataContext : DbContext
    {
        public BaseDataContext(DbContextOptions options, IClaimsPrincipalProvider principalProvider, ILoggerFactory loggerFactory)
            : base(options)
        {
            ClaimsPrincipalProvider = principalProvider;
            Logger = loggerFactory.CreateLogger(GetType());
        }

        public ILogger Logger { get; }
        public IClaimsPrincipalProvider ClaimsPrincipalProvider { get; }
        public ClaimsPrincipal ClaimsPrincipal
        {
            get => ClaimsPrincipalProvider.GetPrincipal();
            set => ClaimsPrincipalProvider.SetPrincipal(value);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=DBSyf;Trusted_Connection=True;");
            
#if DEBUG
            optionsBuilder.EnableSensitiveDataLogging(true);
#endif
        }

        public void SetPrincipal(ClaimsPrincipal principal)
        {
            ClaimsPrincipal = principal;
        }

        public IEnumerable<EntityChange> GetChanges()
        {
            var results = new List<EntityChange>();

            foreach (var item in ChangeTracker.Entries())
            {
                var eventType = GetEventType(item);
                if (eventType == EventType.None) continue;

                var entityType = item.Entity.GetType();
                var change = new EntityChange
                {
                    Entity = item.Entity,
                    EntityType = entityType,
                    EventType = eventType
                };

                // Get the properties that changed
                if (eventType == EventType.Modified)
                {
                    change.Changes = item.Properties
                        .Where(x => x.IsModified)
                        .Select(x => new PropertyChange
                        {
                            Name = x.Metadata.Name,
                            OriginalValue = x.OriginalValue,
                            CurrentValue = x.CurrentValue
                        })
                        .ToList();
                }

                results.Add(change);
            }

            return results;
        }

        public bool HasChanged<TEntity>(TEntity obj, params string[] propertyNames) where TEntity : class
        {
            var trackEntity = Entry(obj);

            return trackEntity.Properties
                .Any(x => propertyNames.Contains(x.Metadata.Name) && x.IsModified);
        }

        public override int SaveChanges()
        {
            InterceptSave();

            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            InterceptSave();

            return await base.SaveChangesAsync(cancellationToken);
        }

        public void Discard(object entity)
        {
            var entry = base.Entry(entity);
            if (entry == null) return;

            switch (entry.State)
            {
                case EntityState.Added:
                    entry.State = EntityState.Detached;
                    break;
                case EntityState.Modified:
                    entry.State = EntityState.Unchanged;
                    break;
                case EntityState.Deleted:
                    entry.State = EntityState.Unchanged;
                    break;
            }
        }

        public void Reset()
        {
            var items = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted ||
                            e.State == EntityState.Unchanged)
                .ToList();

            foreach (var item in items)
            {
                item.State = EntityState.Detached;
            }
        }

        #region InterceptSave

        private void InterceptSave()
        {
            ChangeTracker.DetectChanges();

            var name = ClaimsPrincipal?.Identity?.Name ?? "system";

            foreach (var item in ChangeTracker.Entries().Where(s => s.State == EntityState.Modified || s.State == EntityState.Added))
            {
                var entity = item.Entity;

                if (entity is IAuditEntity audit)
                {
                    if (item.State == EntityState.Added)
                    {
                        audit.CreateUser = name;
                        audit.CreateDate = DateTime.UtcNow;
                    }

                    // Always update the last modified date and last modified user of an object we are saving.
                    audit.ModifyUser = name;
                    audit.ModifyDate = DateTime.UtcNow;
                }
            }

            //Update modify details even when autodetectchanges is false
            if (!ChangeTracker.AutoDetectChangesEnabled)
                ChangeTracker.DetectChanges();
        }

        #endregion

        private EventType GetEventType(EntityEntry entry)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    return EventType.Added;
                case EntityState.Modified:
                    return EventType.Modified;
                case EntityState.Deleted:
                    return EventType.Deleted;
                default:
                    return EventType.None;
            }
        }
    }
}

