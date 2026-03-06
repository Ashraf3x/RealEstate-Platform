namespace RealEstate.Application.DTOs
{
    public class SaleListingDto
    {
        public int ListingId { get; set; }
        public int PropertyId { get; set; }
        public string PropertyTitle { get; set; }
        public decimal PricePerShare { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
    }
}