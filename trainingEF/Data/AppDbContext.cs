using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using trainingEF.Models;
using trainingEF.Models.DTOs;

namespace trainingEF.Data;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<UserDto> UserDtoDbSet => Set<UserDto>();
    public DbSet<Order> OrderDbSet => Set<Order>();
    public DbSet<Product> ProductDbSet => Set<Product>();
    public DbSet<OrderDetail> OrderDetailDbSet => Set<OrderDetail>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Order>()
            .HasOne(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.UserId)
            .IsRequired();

        //modelBuilder.Entity<Order>()
        //    .HasMany(e => e.OrderDetails)
        //    .WithOne()
        //    .HasForeignKey(e => e.OrderId)
        //    .IsRequired();
    }
}
