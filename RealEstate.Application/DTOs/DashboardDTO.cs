using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Application.DTOs
{
    public class DashboardDto
    {
        // Investor 
        public int WalletId { get; set; }
        public decimal WalletBalance { get; set; }
        public decimal TotalInvested { get; set; }
        public int ActiveInvestmentsCount { get; set; }
        public List<PropertyDto> NewProperties { get; set; } = new();
        public List<InvestmentDto> UserInvestments { get; set; } = new();
        public List<WalletTransactionDto> RecentTransactions { get; set; } = new();


        // Admin 
        public int TotalUsers { get; set; }
        public int TotalProperties { get; set; }
        public decimal TotalPlatformInvestments { get; set; }
        public decimal TotalPlatformBalance { get; set; }
        public List<UserDto> RecentUsers { get; set; } = new();
        public List<WalletTransactionDto> RecentPlatformTransactions { get; set; } = new();
    }
}