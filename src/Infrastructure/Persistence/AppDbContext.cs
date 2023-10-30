using SecurityApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace SecurityApi.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext() : base()
    {
    }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Permission> Permissions { get; set; }
    public DbSet<PermissionType> PermissionTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<PermissionType>()
            .HasData(new PermissionType
            {
                Id = 1,
                Description = "Admin"
            },
            new PermissionType
            {
                Id = 2,
                Description = "User"
            });
    }
}
