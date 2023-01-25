using System.Reflection;
using CORE.Entities;
using Microsoft.EntityFrameworkCore;

namespace CORE.DbContexts;

public class MediaContext : DbContext
{
    public MediaContext(DbContextOptions<MediaContext> options) : base(options)
    {
    }


    public DbSet<MediaType> MediaTypes { get; set; } = null!;
    public DbSet<Media> Medias { get; set; } = null!;
    public DbSet<WatchList> WatchLists { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}