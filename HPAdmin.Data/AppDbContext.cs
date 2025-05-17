using HPAdmin.Data.Dto;
using HPAdmin.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HPAdmin.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<HomeTaskDto> HomeTasks { get; set; }
}

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<AppDbContext>();
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(Configuration.ConfigurationSetting)
            .Build();

        builder.UseSqlServer(configuration.GetConnectionString(Configuration.ConnectionString));

        return new AppDbContext(builder.Options);
    }
}


/*
 * Prikazy pro migraci:
 * 1) instalace: dotnet tool install --global dotnet-ef
 * 2) overeni verze: dotnet ef --version
 * 3) incializace migrace: dotnet ef migrations add InitialCreate --project HPAdmin.Data --startup-project HPAdmin.Data
 * 4) provedeni migrace: dotnet ef database update --project HPAdmin.Data --startup-project HPAdmin.Data
 */