using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Domain.Entities
{
    public class YieldDistribution
    {
        [Key]
        public int DistributionId { get; set; }

        [ForeignKey("Property")]
        public int PropertyId { get; set; }

        public decimal Amount { get; set; }
        public DateTime DistributionDate { get; set; }

        [MaxLength(500)]
        public string Notes { get; set; }

        public Property Property { get; set; }
        public ICollection<PaidVia> PaidVias { get; set; }
    }
}