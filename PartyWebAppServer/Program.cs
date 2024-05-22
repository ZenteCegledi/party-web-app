using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PartyWebAppServer.Database;

var builder = WebApplication.CreateBuilder(args);


var Configuration = builder.Configuration;
builder.Services.AddDbContext<AppDbContext>(o =>
    o.UseNpgsql(Configuration.GetConnectionString("TimescaleConnection")));


builder.Services.AddControllersWithViews();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Automapper
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddSwaggerGen();


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
    var maiaContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    maiaContext.Database.Migrate();
}

// app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();



app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapFallbackToFile("index.html");
});

app.Run();
