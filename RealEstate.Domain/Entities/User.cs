using System.ComponentModel.DataAnnotations;

namespace RealEstate.Domain.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(255)]
        public string Password { get; set; }

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