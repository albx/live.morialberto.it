using Microsoft.EntityFrameworkCore;
using MoriAlberto.Live.ReadModel.Model;
using MoriAlberto.Live.ReadModel.Persistence.Comparer;
using MoriAlberto.Live.ReadModel.Persistence.Converters;
using MoriAlberto.Live.ReadModel.Persistence.Mapping;

namespace MoriAlberto.Live.ReadModel.Persistence;

public class LiveDbContext : DbContext
{
    public LiveDbContext(DbContextOptions<LiveDbContext> options)
        : base(options)
    {
    }

    public DbSet<Content> Contents { get; set; }

    public DbSet<Streaming> Streamings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ContentMapper());
        modelBuilder.ApplyConfiguration(new StreamingMapper());
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder
            .Properties<DateOnly>()
            .HaveConversion<DateOnlyConverter, DateOnlyComparer>().HaveColumnType("datetime");

        configurationBuilder.Properties<TimeOnly>()
            .HaveConversion<TimeOnlyConverter, TimeOnlyComparer>().HaveColumnType("time");
    }
}
