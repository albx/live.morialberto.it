using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoriAlberto.Live.Api.Services;
using MoriAlberto.Live.ReadModel.DependencyInjection;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((context, services) =>
    {
        services.AddKittReadModel(context.Configuration.GetConnectionString("KittDatabase")!);
        services.AddScoped<StreamingsService>();
    })
    .Build();

host.Run();
