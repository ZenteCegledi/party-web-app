using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using PartyWebAppClient;
using PartyWebAppClient.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var baseUri = new Uri(builder.HostEnvironment.BaseAddress);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddTransient<IAppHttpClient, AppHttpClient>();

builder.Services.AddBlazorBootstrap();

builder.Services.AddTransient(sp => new HubConnectionBuilder()
    .WithUrl(new Uri(baseUri, "/hub").AbsoluteUri)
    .WithAutomaticReconnect()
    .Build());

await builder.Build().RunAsync();