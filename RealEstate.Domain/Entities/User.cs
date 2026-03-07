using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
namespace RealEstate.Domain.Entities
{
    public class User : IdentityUser<int>
    {
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(20)]
        public string Role { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        public Wallet Wallet { get; set; }
        public ICollection<KycDocument> KycDocuments { get; set; }
        public ICollection<Investment> Investments { get; set; }
        public ICollection<UserSaleListing> UserSaleListings { get; set; }
        public ICollection<UserPropertyContract> UserPropertyContracts { get; set; }
    }
}