using SecurityApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace SecurityApi.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<PermissionType> PermissionTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Data Source=.\\SQLExpress;Initial Catalog=OPERACIONES_UNI;Persist Security Info=True;User ID=User_Operaciones_Uni_desa;Password=SDlqtFq7kZXLK1dQ18wN;TrustServerCertificate=True");
        }
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
