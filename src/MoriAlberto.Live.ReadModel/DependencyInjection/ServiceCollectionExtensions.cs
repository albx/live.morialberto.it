using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MoriAlberto.Live.ReadModel.Persistence;

namespace MoriAlberto.Live.ReadModel.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddKittReadModel(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<LiveDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<IDatabase, Database>();

        return services;
    }
}
