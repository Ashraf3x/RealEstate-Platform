using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entities;

namespace RealEstate.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<KycDocument> KycDocuments { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyDocument> PropertyDocuments { get; set; }
        public DbSet<Investment> Investments { get; set; }
        public DbSet<YieldDistribution> YieldDistributions { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletTransaction> WalletTransactions { get; set; }
        public DbSet<PaidVia> PaidVias { get; set; }
        public DbSet<SettledVia> SettledVias { get; set; }
        public DbSet<SaleListing> SaleListings { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<UserPropertyContract> UserPropertyContracts { get; set; }
        public DbSet<UserSaleListing> UserSaleListings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaidVia>()
                .HasKey(p => new { p.WalletId, p.TransactionId });

            modelBuilder.Entity<SettledVia>()
                .HasKey(s => new { s.ListingId, s.WalletId, s.TransactionId });

            modelBuilder.Entity<UserPropertyContract>()
                .HasKey(u => new { u.UserId, u.PropertyId });

            modelBuilder.Entity<UserSaleListing>()
                .HasKey(u => new { u.UserId, u.ListingId });

            modelBuilder.Entity<Purchase>()
                .HasKey(p => p.ListingId);

            modelBuilder.Entity<Wallet>()
                .HasIndex(w => w.UserId)
                .IsUnique();
        }
    }
}