using System.ComponentModel.DataAnnotations;
namespace RealEstate.Domain.Entities
{
    public class Contract
    {
        [Key]
        public int ContractId { get; set; }

        [MaxLength(255)]
        public string FilePath { get; set; }

        public ICollection<UserPropertyContract> UserPropertyContracts { get; set; }
    }
}
