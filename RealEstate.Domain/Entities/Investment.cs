using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Domain.Entities
{
    public class Investment
    {
        [Key]
        public int InvestmentId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Property")]
        public int PropertyId { get; set; }

        public int ShareCount { get; set; }
        public decimal OwnershipPercentage { get; set; }
        public DateTime PurchasedAt { get; set; }

        public User User { get; set; }
        public Property Property { get; set; }
    }
}