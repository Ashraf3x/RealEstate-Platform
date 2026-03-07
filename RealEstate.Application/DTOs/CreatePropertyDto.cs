namespace RealEstate.Application.DTOs
{
    public class CreatePropertyDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int TotalShares { get; set; }
        public decimal PricePerShare { get; set; }
        public string Status { get; set; }
    }
}