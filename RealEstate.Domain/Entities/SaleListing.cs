using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Domain.Entities
{
    public class SaleListing
    {
        [Key]
        public int ListingId { get; set; }

        [ForeignKey("Property")]
        public int PropertyId { get; set; }

        public decimal PricePerShare { get; set; }
        public int Quantity { get; set; }

        [MaxLength(20)]
        public string Status { get; set; }

        public Property Property { get; set; }
        public Purchase Purchase { get; set; }
        public SettledVia SettledVia { get; set; }
        public ICollection<UserSaleListing> UserSaleListings { get; set; }
    }
}