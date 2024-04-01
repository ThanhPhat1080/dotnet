using Employee.Models;
using Microsoft.EntityFrameworkCore;

namespace Management.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> _options) : base(_options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TrainingEmployee;Trusted_Connection=True;");
        }

        public DbSet<EmployeeModel> EmployeesDbSet { get; set; }
    }
}
