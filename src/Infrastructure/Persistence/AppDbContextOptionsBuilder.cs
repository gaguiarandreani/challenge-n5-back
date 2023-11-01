using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SecurityApi.Infrastructure.Persistence;

public class AppDbContextOptionsBuilder
{
    private readonly IConfiguration _config;

    public AppDbContextOptionsBuilder(IConfiguration config)
    {
        _config = config;
    }

    public DbContextOptionsBuilder<AppDbContext> CreateNew()
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        //var connectionString = _config.GetConnectionString("AppDbContext");

        optionsBuilder.UseSqlServer("Data Source=DBOPRCNSUNIDESA.andreani.com.ar;Initial Catalog=OPERACIONES_UNI;Persist Security Info=True;User ID=User_Operaciones_Uni_desa;Password=SDlqtFq7kZXLK1dQ18wN;TrustServerCertificate=True");

        return optionsBuilder;
    }
}