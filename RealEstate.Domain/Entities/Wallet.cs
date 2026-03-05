using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RealEstate.Domain.Entities
{
    public class Wallet
    {
        [Key]
        public int WalletId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }

        public User User { get; set; } 
        public ICollection<WalletTransaction> WalletTransactions { get; set; }

    }
}
