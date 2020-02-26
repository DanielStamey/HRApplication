using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace EmployeeManager
{
    public class DataContext : DbContext
    {
        public DataContext()
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<EmploymentStatus> EmploymentStatuss { get; set; }

        public DbSet<Shift> Shifts { get; set; }

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<EmployeePermission> EmployeePermissions { get; set; }

        public DbSet<AuditLog> AuditLogs { get; set; }

        public override int SaveChanges()
        {
            var modifiedEntities = ChangeTracker.Entries()
                .Where(p => p.State == EntityState.Modified && p.Entity.GetType() == typeof(Employee)).ToList();
            var now = DateTime.UtcNow;

            foreach (var change in modifiedEntities)
            {
                var entityName = change.Entity.GetType().Name;
                var primaryKey = GetPrimaryKeyValue(change);

                foreach (var prop in change.OriginalValues.PropertyNames)
                {
                    var originalValue = change.GetDatabaseValues().GetValue<object>(prop)?.ToString();
                    var currentValue = change.CurrentValues[prop]?.ToString();
                    if (originalValue != currentValue)
                    {
                        AuditLog log = new AuditLog()
                        {
                            EntityName = entityName,
                            PrimaryKeyValue = primaryKey.ToString(),
                            PropertyName = prop,
                            OldValue = originalValue,
                            NewValue = currentValue,
                            DateChanged = now
                        };
                        AuditLogs.Add(log);
                    }
                }
            }
            return base.SaveChanges();
        }

        private object GetPrimaryKeyValue(DbEntityEntry entry)
        {
            var objectStateEntry = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.GetObjectStateEntry(entry.Entity);
            return objectStateEntry.EntityKey.EntityKeyValues[0].Value;
        }
    }
}