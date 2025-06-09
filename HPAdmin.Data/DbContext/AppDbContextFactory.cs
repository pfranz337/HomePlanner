using HomePlanner.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HomePlanner.Data.DbContext;

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