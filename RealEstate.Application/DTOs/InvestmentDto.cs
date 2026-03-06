using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Application.DTOs
{
    public class InvestmentDto
    {
        public int InvestmentId;
        public string UserName;
        public string PropertyName;
        public int ShareCount;
        public decimal OwnershipPercentage;
        public DateTime PurchasedAt;
    }
}
