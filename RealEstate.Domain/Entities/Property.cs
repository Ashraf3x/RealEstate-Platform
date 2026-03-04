using System.ComponentModel.DataAnnotations;

namespace RealEstate.Domain.Entities
{
    public class Property
    {
        [Key]
        public int PropertyId { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [MaxLength(200)]
        public string Location { get; set; }

        public int TotalShares { get; set; }
        public decimal PricePerShare { get; set; }

        [MaxLength(20)]
        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<PropertyDocument> PropertyDocuments { get; set; }
        public ICollection<Investment> Investments { get; set; }
        public ICollection<YieldDistribution> YieldDistributions { get; set; }
        public ICollection<UserPropertyContract> UserPropertyContracts { get; set; }
        public ICollection<SaleListing> SaleListings { get; set; }
    }
}