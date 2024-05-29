using Microsoft.EntityFrameworkCore;
using PartyWebAppServer.Database;
using BitzArt.Blazor.Auth;
using PartyWebAppServer.Services.AuthService;
using PartyWebAppServer.Services.EventService;
using PartyWebAppServer.Services.JwtService;
using PartyWebAppServer.Services.LocationService;
using PartyWebAppServer.Services.UserService;
using PartyWebAppServer.Services.WalletService;
using IUserService = PartyWebAppServer.Services.UserService.IUserService;

var builder = WebApplication.CreateBuilder(args);

var Configuration = builder.Configuration;
builder.Services.AddDbContext<AppDbContext>(o =>
    o.UseNpgsql(Configuration.GetConnectionString("TimescaleConnection")));


// add configuratuion from appsettings
builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.AddControllersWithViews();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Automapper
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddSwaggerGen();

builder.AddBlazorAuth<AuthService>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole",
        policy => policy.RequireAssertion(context =>
            context.User.IsInRole("Admin") ||
            context.User.HasClaim(claim => claim.Type == "IsAdmin" && claim.Value == "true")
        )
    );
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<IJwtService, JwtService>();
builder.Services.AddTransient<ILocationService, LocationService>();
builder.Services.AddTransient<IWalletService, WalletService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IEventService, EventService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Apply migrations
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

// app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();

app.UseAntiforgery();

app.MapControllers();
app.MapAuthEndpoints();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapFallbackToFile("index.html");
});

app.Run();
