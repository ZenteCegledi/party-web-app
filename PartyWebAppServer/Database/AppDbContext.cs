using Microsoft.EntityFrameworkCore;
using PartyWebAppServer.Database.Models;

namespace PartyWebAppServer.Database;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public AppDbContext()
    {
    }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().HasKey(u => u.Username);
        modelBuilder.UseSerialColumns();
    }
}