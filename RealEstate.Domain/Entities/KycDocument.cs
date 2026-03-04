using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Domain.Entities
{
    public class KycDocument
    {
        [Key]
        public int KycId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [MaxLength(50)]
        public string DocumentType { get; set; }

        [MaxLength(255)]
        public string FilePath { get; set; }

        [MaxLength(20)]
        public string Status { get; set; }

        public DateTime UploadedAt { get; set; }

        public User User { get; set; }
    }
}