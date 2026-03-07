using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RealEstate.Application.DTOs
{
    public class WalletTransactionDto
    {
        public int TransactionId { get; set; }
        public int WalletId { get; set; }

        [Required]
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }

    }
}

