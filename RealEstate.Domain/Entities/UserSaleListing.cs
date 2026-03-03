namespace RealEstate.Domain.Entities
{
    internal class UserSaleListing
    {
        public int UserId { get; set; }
        public int ListingId { get; set; }
        public User User { get; set; }
        public SaleListing SaleListing { get; set; }
    }
}
