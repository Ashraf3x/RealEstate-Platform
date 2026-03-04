using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RealEstate.Domain.Entities
{
    public class WalletTransaction
    {
        [ Column(Order = 0)]
        [ForeignKey("Wallet")]
        public int WalletId { get; set; }

        [ Column(Order = 1)]
        public int TransactionId { get; set; }

        [MaxLength(50)]
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }


        public Wallet Wallet { get; set; }
    }
}
