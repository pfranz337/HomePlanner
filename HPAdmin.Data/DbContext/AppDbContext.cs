using HPAdmin.Data.Dto;
using Microsoft.EntityFrameworkCore;

namespace HPAdmin.Data.DbContext;

public class AppDbContext(DbContextOptions<AppDbContext> options) : Microsoft.EntityFrameworkCore.DbContext(options)
{
    public DbSet<HomeTaskDto> HomeTasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .AddInterceptors(new DtoMaterializationInterceptor());
    }
}