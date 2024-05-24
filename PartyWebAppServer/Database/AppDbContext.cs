using Microsoft.EntityFrameworkCore;
using PartyWebAppCommon.enums;
using PartyWebAppServer.Database.Models;

namespace PartyWebAppServer.Database;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Wallet> Wallets { get; set; }

    public DbSet<Locations> Locations { get; set; }

    public DbSet<Event> Events { get; set; }

    public DbSet<Role> Roles { get; set; }

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

        modelBuilder.Entity<Locations>().HasKey(l => l.Id);

        modelBuilder.Entity<Role>().HasKey(r => r.Id);
        modelBuilder.Entity<Role>().Property(r => r.Name).HasConversion<string>();


        // Seed data ========================================

        // Roles
        modelBuilder.Entity<Role>().HasData(new Role { Id = 1, Name = RoleType.Admin });
        modelBuilder.Entity<Role>().HasData(new Role { Id = 2, Name = RoleType.User });

        // Users
        // modelBuilder.Entity<User>().HasData(new User { Username = "admin", Password = "admin", RoleId = 1, Email = "admin@admin.com"});

    }
}