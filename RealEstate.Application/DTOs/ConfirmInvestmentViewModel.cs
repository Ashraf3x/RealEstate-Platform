namespace RealEstate.Application.DTOs
{
    public class ConfirmInvestmentViewModel
    {
        public int PropertyId { get; set; }
        public string PropertyTitle { get; set; }
        public int Shares { get; set; }
        public decimal PricePerShare { get; set; }
        public decimal TotalCost { get; set; }
    }
}