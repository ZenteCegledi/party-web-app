using Microsoft.EntityFrameworkCore;
using PartyWebAppCommon.Enums;
using PartyWebAppServer.Database.Models;
using BCrypt.Net;

namespace PartyWebAppServer.Database;

public class AppDbContext : DbContext
{

    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<User> Users { get; set; }

    public DbSet<Location> Locations { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
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

        #region RoleSeed

        modelBuilder.Entity<Role>().HasData(new Role { Id = 1, Name = RoleType.Admin });
        modelBuilder.Entity<Role>().HasData(new Role { Id = 2, Name = RoleType.User });

        #endregion


        #region UserSeed

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Password = BCrypt.Net.BCrypt.HashPassword("admin"),
                Username = "admin",
                RoleId = 1,
                Email = "admin@admin.com",
                Name = "Admin User",
                BirthDate = DateTime.UtcNow.AddYears(-30),
                Phone = "1234567890",
                PasswordUpdated = DateTime.UtcNow,
            }
        );

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Password = BCrypt.Net.BCrypt.HashPassword("user"),
                Username = "user",
                RoleId = 2,
                Email = "user@gmail.com",
                Name = "User",
                BirthDate = DateTime.UtcNow.AddYears(-20),
                Phone = "0987654321",
                PasswordUpdated = DateTime.UtcNow,
            }
        );

        #endregion

        #region WalletSeed

        modelBuilder.Entity<Wallet>().HasData(
            new Wallet
            {
                Currency = CurrencyType.EUR,
                Username = "user",
                Amount = 100
            }
        );

        modelBuilder.Entity<Wallet>().HasData(
            new Wallet
            {
                Currency = CurrencyType.USD,
                Username = "user",
                Amount = 400
            }
        );

        modelBuilder.Entity<Wallet>().HasData(
            new Wallet
            {
                Currency = CurrencyType.HUF,
                Username = "user",
                Amount = 5000,
                IsPrimary = true
            }
        );

        modelBuilder.Entity<Wallet>().HasData(
            new Wallet
            {
                Currency = CurrencyType.CREDIT,
                Username = "user",
                Amount = 10000
            }
        );

        #endregion
    }
}