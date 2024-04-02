using Microsoft.EntityFrameworkCore;
using trainingEF.Models;

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
