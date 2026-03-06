namespace RealEstate.Application.DTOs
{
    public class CreateSaleListingDto
    {
        public int PropertyId { get; set; }
        public decimal PricePerShare { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
    }
}