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

        modelBuilder.Entity<Transaction>().HasOne(t => t.Location).WithMany(l => l.Transactions).HasForeignKey(t => t.LocationId);
        modelBuilder.Entity<Transaction>().HasOne(t => t.Event).WithMany(e => e.Transactions).HasForeignKey(t => t.EventId);
        modelBuilder.Entity<Transaction>().HasOne(t => t.Wallet).WithMany(w => w.Transactions).HasForeignKey(t => t.Wallet);

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

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Password = BCrypt.Net.BCrypt.HashPassword("user2"),
                Username = "user2",
                RoleId = 2,
                Email = "user2@gmail.com",
                Name = "User2",
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

        modelBuilder.Entity<Wallet>().HasData(
            new Wallet
            {
                Currency = CurrencyType.HUF,
                Username = "user2",
                Amount = 10000,
                IsPrimary = true
            }
        );

        #endregion

        #region LocationSeed

        modelBuilder.Entity<Location>().HasData(
            new Location
            {
                Id = 1,
                Name = "Club event 1",
                Type = LocationType.Club,
                Address = "Budapest, Váci út 1"
            }
        );

        modelBuilder.Entity<Location>().HasData(
            new Location
            {
                Id = 2,
                Name = "Pub event 1",
                Type = LocationType.Pub,
                Address = "Budapest, Váci út 2"
            }
        );

        modelBuilder.Entity<Location>().HasData(
            new Location
            {
                Id = 3,
                Name = "ATM event 1",
                Type = LocationType.ATM,
                Address = "Budapest, Váci út 3"
            }
        );

        modelBuilder.Entity<Location>().HasData(
            new Location
            {
                Id = 4,
                Name = "Theater event 1",
                Type = LocationType.Theater,
                Address = "Budapest, Váci út 4"
            }
        );

        modelBuilder.Entity<Location>().HasData(
            new Location
            {
                Id = 5,
                Name = "Museum event 1",
                Type = LocationType.Museum,
                Address = "Budapest, Váci út 5"
            }
        );

        #endregion

        #region EventSeed

        modelBuilder.Entity<Event>().HasData(
            new Event
            {
                Id = 1,
                Name = "Event 1",
                StartDateTime = new DateTime(2024, 5, 31, 20, 0, 0).ToUniversalTime(),
                EndDateTime = new DateTime(2024, 6, 1, 6, 0, 0).ToUniversalTime(),

                LocationId = 1,
                Description = "This is the description of Event 1. It is a very cool event in the club.",
                Price = 1000
            }
        );

        modelBuilder.Entity<Event>().HasData(
            new Event
            {
                Id = 2,
                Name = "Event 2",
                StartDateTime = new DateTime(2024, 6, 1, 20, 0, 0).ToUniversalTime(),
                EndDateTime = new DateTime(2024, 6, 2, 6, 0, 0).ToUniversalTime(),

                LocationId = 2,
                Description = "This is the description of Event 2. It is a very cool event in the pub.",
                Price = 2000
            }
        );

        #endregion
    }
}