using System.ComponentModel.DataAnnotations.Schema;
namespace RealEstate.Domain.Entities
{
    public class PaidVia
    {
        [Column(Order = 0)]
        [ForeignKey("Wallet")]
        public int WalletId { get; set; }

        [Column(Order = 1)]
        [ForeignKey("WalletTransaction")]
        public int TransactionId { get; set; }

        [ForeignKey("YieldDistribution")]
        [Column(Order = 2)]
        public int DistributionId { get; set; }

        public Wallet Wallet { get; set; }
        public WalletTransaction WalletTransaction { get; set; }
        public YieldDistribution YieldDistribution { get; set; } // Awaiting Yield_Distribution Class to be implemented

    }
}
