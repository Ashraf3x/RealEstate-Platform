using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Application.DTOs
{
    public class WalletDto
    {
        public int WalletId { get; set; }
        public int UserId { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<WalletTransactionDto> RecentTransactions { get; set; } = new();
    }
}
