using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.FluentUI.AspNetCore.Components;
using PartyWebAppClient;
using PartyWebAppClient.Services;
using BitzArt.Blazor.Auth;
using BitzArt.Blazor.Cookies;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var baseUri = new Uri(builder.HostEnvironment.BaseAddress);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddTransient<IAppHttpClient, AppHttpClient>();
builder.Services.AddTransient<IClientWalletService, ClientWalletService>();

builder.Services.AddBlazorBootstrap();

builder.AddBlazorCookies();
builder.AddBlazorAuth();

builder.Services.AddTransient(sp => new HubConnectionBuilder()
    .WithUrl(new Uri(baseUri, "/hub").AbsoluteUri)
    .WithAutomaticReconnect()
    .Build());

builder.Services.AddFluentUIComponents();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddAuthorizationCore(options =>
{
    options.AddPolicy("RequireAdminRole",
        policy => policy.RequireAssertion(context =>
            context.User.IsInRole("Admin") ||
            context.User.HasClaim(claim => claim.Type == "IsAdmin" && claim.Value == "true")
        )
    );
});

await builder.Build().RunAsync();