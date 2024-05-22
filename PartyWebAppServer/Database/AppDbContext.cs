using Microsoft.EntityFrameworkCore;
using PartyWebAppServer.Database.Models;

namespace PartyWebAppServer.Database;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Location> Locations { get; set; }

    public DbSet<Event> Events { get; set; }

    public DbSet<Role> Roles { get; set; }

    public DbSet<Role> Roles { get; set; }

    public DbSet<Transaction> Transactions { get; set; }
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

        modelBuilder.Entity<Transaction>().HasKey(t => t.Id);
        modelBuilder.Entity<Role>().HasKey(r => r.Id);
        modelBuilder.Entity<Role>().Property(r => r.Name).HasConversion<string>();


        modelBuilder.Entity<Role>().HasKey(r => r.Id);

    }
}