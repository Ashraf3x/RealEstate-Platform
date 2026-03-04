using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Domain.Entities
{
    public class UserSaleListing
    {
        [ Column(Order = 0)]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [ Column(Order = 1)]
        [ForeignKey("SaleListing")]
        public int ListingId { get; set; }

        public User User { get; set; }
        public SaleListing SaleListing { get; set; }
    }
}
