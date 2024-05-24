using Microsoft.EntityFrameworkCore;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.enums;
using PartyWebAppServer.Database.Models;

namespace PartyWebAppServer.Database;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Role> Roles { get; set; }
    
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
        
        modelBuilder.Entity<Location>().HasKey(l => l.Id);
        
        modelBuilder.Entity<Transaction>().HasKey(t => t.Id);
        modelBuilder.Entity<Role>().HasKey(r => r.Id);
        modelBuilder.Entity<Role>().Property(r => r.Name).HasConversion<string>();

        modelBuilder.Entity<Location>().HasKey(l => l.Id);

        modelBuilder.Entity<Role>().HasKey(r => r.Id);
        modelBuilder.Entity<Role>().Property(r => r.Name).HasConversion<string>();


        // Seed data ========================================

        #region RoleSeed

        modelBuilder.Entity<Role>().HasData(new Role { Id = 1, Name = RoleType.Admin });
        modelBuilder.Entity<Role>().HasData(new Role { Id = 2, Name = RoleType.User });

        #endregion

        // Users
        // modelBuilder.Entity<User>().HasData(new User { Username = "admin", Password = "admin", RoleId = 1, Email = "admin@admin.com"});
        // create a new user using the UserDto

        #region UserSeed

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Password = "admin",
                Username = "admin",
                RoleId = 1,
                Email = "admin@admin.com",
                Name = "Admin User",
                BirthDate = DateTime.UtcNow.AddYears(-30),
                Phone = "1234567890",
                PasswordUpdated = DateTime.UtcNow,
                Wallets = []
            }
        );

        #endregion
    }
}