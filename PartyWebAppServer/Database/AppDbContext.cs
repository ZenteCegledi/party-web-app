using Microsoft.EntityFrameworkCore;
using PartyWebAppServer.Database.Models;

namespace PartyWebAppServer.Database;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Location> Locations { get; set; }
    
    public DbSet<Event> Events { get; set; }
    
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

        modelBuilder.Entity<User>().HasKey(u => u.Username);
        modelBuilder.Entity<Wallet>().HasKey(w => new { w.Currency, w.Username });
        modelBuilder.Entity<Wallet>().HasOne(w => w.Owner).WithMany(u => u.Wallets).HasForeignKey(w => w.Username);
        
        modelBuilder.Entity<Location>().HasKey(l => l.Id);
        
    }
}