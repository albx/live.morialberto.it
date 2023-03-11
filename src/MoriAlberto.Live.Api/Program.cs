using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoriAlberto.Live.Api.Configuration;
using MoriAlberto.Live.Api.Services;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((context, services) =>
    {
        services.Configure<KittDatabaseConfiguration>(options =>
        {
            options.ConnectionString = context.Configuration.GetConnectionString("KittDatabase");
        });

        services.AddScoped<StreamingsService>();
    })
    .Build();

host.Run();
