namespace RealEstate.Domain.Entities
{
    public class UserSaleListing
    {
        public int UserId { get; set; }
        public int ListingId { get; set; }
        public User User { get; set; }
        public SaleListing SaleListing { get; set; }
    }
}
