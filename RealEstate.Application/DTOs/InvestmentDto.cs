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
        public decimal AnnualYield { get; set; }
        public int OccupancyRate { get; set; }
        public string AppreciationStatus { get; set; }
        public int AppreciationProgress { get; set; }
    }
}
