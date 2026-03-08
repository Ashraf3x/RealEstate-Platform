using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entities;

namespace RealEstate.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

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
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<WalletTransaction>().HasKey(wt => wt.TransactionId);
            modelBuilder.Entity<PaidVia>().HasKey(p => new { p.WalletId, p.TransactionId, p.DistributionId });
            modelBuilder.Entity<SettledVia>().HasKey(s => new { s.ListingId, s.WalletId, s.TransactionId });
            modelBuilder.Entity<UserPropertyContract>().HasKey(u => new { u.UserId, u.PropertyId, u.ContractId });
            modelBuilder.Entity<UserSaleListing>().HasKey(u => new { u.UserId, u.ListingId });
            modelBuilder.Entity<Purchase>().HasKey(p => p.ListingId);
            modelBuilder.Entity<Wallet>().HasIndex(w => w.UserId).IsUnique();

            modelBuilder.Entity<Investment>().Property(i => i.OwnershipPercentage).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Property>().Property(p => p.PricePerShare).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<SaleListing>().Property(s => s.PricePerShare).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Wallet>().Property(w => w.Balance).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<WalletTransaction>().Property(w => w.Amount).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<YieldDistribution>().Property(y => y.Amount).HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PaidVia>()
                .HasOne(p => p.Wallet).WithMany()
                .HasForeignKey(p => p.WalletId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<PaidVia>()
                .HasOne(p => p.WalletTransaction).WithMany()
                .HasForeignKey(p => p.TransactionId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<PaidVia>()
                .HasOne(p => p.YieldDistribution).WithMany("PaidVias")
                .HasForeignKey(p => p.DistributionId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SettledVia>()
                .HasOne(s => s.Wallet).WithMany()
                .HasForeignKey(s => s.WalletId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<SettledVia>()
                .HasOne(s => s.WalletTransaction).WithMany()
                .HasForeignKey(s => s.TransactionId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Property>().HasData(
        new Property
        {
            PropertyId = 1,
            Title = "Smart Apartment in New Cairo",
            Description = "A modern smart apartment with 3D viewing.",
            PricePerShare = 50m, 
            TotalShares = 1000,
            Location = "New Cairo",
            Status = "Active",
            CreatedAt = new DateTime(2026, 3, 1, 0, 0, 0, DateTimeKind.Utc) 
        },
        new Property
        {
            PropertyId = 2,
            Title = "Fractional Real Estate Office",
            Description = "Premium office space in the administrative capital.",
            PricePerShare = 100m,
            TotalShares = 500,
            Location = "New Capital",
            Status = "Active",
            CreatedAt = new DateTime(2026, 3, 5, 0, 0, 0, DateTimeKind.Utc)
        }
    );
        }
    }
}