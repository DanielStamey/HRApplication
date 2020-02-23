using System.Data.Entity;

namespace EmployeeManager
{
    public class DataContext:DbContext
    {
        public DataContext()
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<EmploymentStatus> EmploymentStatuss { get; set; }

        public System.Data.Entity.DbSet<EmployeeManager.Shift> Shifts { get; set; }
    }
}