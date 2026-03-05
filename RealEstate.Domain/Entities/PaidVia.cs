using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace RealEstate.Domain.Entities
{
    public class PaidVia
    {
        [ Column(Order = 0)]
        [ForeignKey("Wallet")]
        public int WalletId { get; set; }

        [Column(Order = 1)]
        [ForeignKey("WalletTransaction")]
        public int TransactionId { get; set; }

        [ForeignKey("YieldDistribution")]
        public int DistributionId { get; set; }

        public Wallet Wallet { get; set; }
        public WalletTransaction WalletTransaction { get; set; }
        public YieldDistribution YieldDistribution { get; set; } 

    }
}
