using Microsoft.EntityFrameworkCore;
using EmployeesApp.Models;

namespace EmployeesApp.Models
{
    public class HRDatabaseContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"data source=DESKTOP-4IB66C6\sqlexpress; initial catalog=EmployeesDB; integrated security=SSPI;");
        }
    }
}
