using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using trainingEF.Models;
using trainingEF.Models.DTOs;

namespace trainingEF.Data;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<UserDto>? UserDtoDbSet { get; set; }
    public DbSet<Order>? OrderDbSet { get; set; }
    public DbSet<Product>? ProductDbSet { get; set; }
    public DbSet<OrderDetail>? OrderDetailDbSet { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
            .HasOne(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.UserId)
            .IsRequired();

        modelBuilder.Entity<Order>()
            .HasMany(e => e.OrderDetails)
            .WithOne()
            .HasForeignKey(e => e.OrderId)
            .IsRequired();
    }
}
