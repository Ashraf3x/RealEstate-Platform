using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Application.DTOs
{
    public class InvestmentDto
    {
        public int InvestmentId { set; get; }
        public string UserName { set; get; }
        public string PropertyName { set; get; }
        public int ShareCount { set; get; }
        public decimal OwnershipPercentage { set; get; }
        public DateTime PurchasedAt { set; get; }
    }
}
