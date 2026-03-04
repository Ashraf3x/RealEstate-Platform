using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Domain.Entities
{
    public class UserPropertyContract
    {
        [ Column(Order = 0)]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [ Column(Order = 1)]
        [ForeignKey("Property")]
        public int PropertyId { get; set; }

        [ForeignKey("Contract")]
        public int ContractId { get; set; }

        public User User { get; set; }
        public Property Property { get; set; }
        public Contract Contract { get; set; }
    }
}
