using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Infrastructure.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Investment> Investments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PaidVia>()
                .HasKey(pv => new { pv.WalletId, pv.TransactionId });
            modelBuilder.Entity<SettledVia>()
                .HasKey(sv => new { sv.ListingId, sv.WalletId, sv.TransactionId });
            modelBuilder.Entity<UserPropertyContract>()
            .HasKey(upc => new { upc.UserId, upc.PropertyId });
            modelBuilder.Entity<UserSaleListing>()
        .HasKey(usl => new { usl.UserId, usl.ListingId });
            modelBuilder.Entity<WalletTransaction>()
        .HasKey(wt => new { wt.WalletId, wt.TransactionId });

            modelBuilder.Entity<PaidVia>()
        .HasOne(pv => pv.WalletTransaction)
        .WithMany() 
        .HasForeignKey(pv => new { pv.WalletId, pv.TransactionId })
        .OnDelete(DeleteBehavior.NoAction); 

            modelBuilder.Entity<SettledVia>()
                .HasOne(sv => sv.WalletTransaction)
                .WithMany()
                .HasForeignKey(sv => new { sv.WalletId, sv.TransactionId })
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

}
