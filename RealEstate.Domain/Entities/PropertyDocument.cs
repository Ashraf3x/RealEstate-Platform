using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Domain.Entities
{
    public class PropertyDocument
    {
        [Key]
        public int DocId { get; set; }

        [ForeignKey("Property")]
        public int PropertyId { get; set; }

        [MaxLength(50)]
        public string DocumentType { get; set; }

        [MaxLength(255)]
        public string FilePath { get; set; }

        public DateTime UploadedAt { get; set; }

        public Property Property { get; set; }
    }
}