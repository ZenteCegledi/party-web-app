using Microsoft.EntityFrameworkCore;

namespace PartyWebAppServer.Database;

public class AppDbContext : DbContext
{

    public AppDbContext()
    {
    }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.UseSerialColumns();
    }
}