using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Domain.Entities
{
    public class Purchase
    {
        [Key]
        [ForeignKey("SaleListing")]
        public int ListingId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public DateTime PurchasedAt { get; set; }

        public SaleListing SaleListing { get; set; }
        public User User { get; set; }
    }
}