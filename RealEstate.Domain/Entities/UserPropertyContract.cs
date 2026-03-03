namespace RealEstate.Domain.Entities
{
    public class UserPropertyContract
    {
        public int UserId { get; set; }
        public int PropertyId { get; set; }
        public int ContractId { get; set; }
        public User User { get; set; }
        public Property Property { get; set; }
        public Contract Contract { get; set; }
    }
}
