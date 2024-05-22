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

        modelBuilder.Entity<Wallet>().HasKey(w => new { w.Currency, w.UserID });
        modelBuilder.Entity<Wallet>().HasOne(w => w.Owner).WithMany(u => u.Wallets).HasForeignKey(w => w.UserID);
    }
}