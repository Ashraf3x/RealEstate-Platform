using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RealEstate.Domain.Entities
{
    public class SettledVia
    {
        [ Column(Order = 0)]
        [ForeignKey("SaleListing")]
        public int ListingId { get; set; }

        [ Column(Order = 1)]
        [ForeignKey("Wallet")]
        public int WalletId { get; set; }

        [ Column(Order = 2)]
        [ForeignKey("WalletTransaction")]
        public int TransactionId { get; set; }


        public SaleListing SaleListing { get; set; } // Awatiing Sale Listing Class to be implemented
        public Wallet Wallet { get; set; }
        public WalletTransaction WalletTransaction { get; set; }
    }
}
