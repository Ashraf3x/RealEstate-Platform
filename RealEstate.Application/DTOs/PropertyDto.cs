namespace RealEstate.Application.DTOs
{
    public class PropertyDto
    {
        public int PropertyId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int TotalShares { get; set; }
        public decimal PricePerShare { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal? AnnualYield { get; set; }
        public int? OccupancyRate { get; set; }
        public string? AppreciationStatus { get; set; }
        public int? AppreciationProgress { get; set; }
    }
}