using Microsoft.EntityFrameworkCore;
using trainingEF.Models;

namespace trainingEF.Data
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration configuration;

        public AppDbContext(DbContextOptions options, IConfiguration _configuration) : base(options)
        {
            configuration = _configuration;
        }

        public DbSet<UserModel>? UserDbSet { get; set; }
        public DbSet<Customer>? Customers { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<OrderDetail>? OrderDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Default"));
        }
    }

}
