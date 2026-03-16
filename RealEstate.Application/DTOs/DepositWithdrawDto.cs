using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Application.DTOs
{
    public class DepositDto
    {
        public int WalletId { get; set; }
        public decimal Amount { get; set; }
    }

    public class WithdrawDto
    {
        public int WalletId { get; set; }
        public decimal Amount { get; set; }
    }
}