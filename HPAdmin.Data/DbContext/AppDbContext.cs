using HomePlanner.Shared.Dto;
using HomePlanner.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HomePlanner.Data.DbContext;
public sealed class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<HomeTaskDto> HomeTasks { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        ChangeTracker.Tracked += onEntityTracked;
    }

    private void onEntityTracked(object? sender, EntityTrackedEventArgs e)
    {
        if (e.Entry.Entity is DtoDataBase dto && e.FromQuery)
        {
            dto.State = DtoState.Clean;
        }
    }
}