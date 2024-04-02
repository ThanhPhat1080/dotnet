using Microsoft.EntityFrameworkCore;

namespace trainingEF
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserModel> UserDbSet { get; set; }
    }

}
