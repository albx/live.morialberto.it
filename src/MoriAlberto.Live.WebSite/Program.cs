using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MoriAlberto.Live.WebSite;
using MoriAlberto.Live.WebSite.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddHttpClient<StreamingsService>(
//    client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

builder.Services
    .AddStreamingsClient()
    .ConfigureHttpClient(client => client.BaseAddress = new Uri($"{builder.HostEnvironment.BaseAddress}data-api/graphql"));

builder.Services.AddScoped<StreamingsService>();

await builder.Build().RunAsync();
