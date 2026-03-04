using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Services
{
    public class InvestmentService
    {
        private readonly IInvestmentRepository _investmentRepository;

        public InvestmentService(IInvestmentRepository investmentRepository)
        {
            _investmentRepository = investmentRepository;
        }

        public async Task<Investment> CreateInvestmentAsync(int userId, int propertyId, int shareCount, decimal totalPropertyShares)
        {
            decimal ownershipPercentage = (shareCount / totalPropertyShares) * 100;

            var newInvestment = new Investment
            {
                UserId = userId,
                PropertyId = propertyId,
                ShareCount = shareCount,
                OwnershipPercentage = ownershipPercentage,
                PurchasedAt = DateTime.UtcNow
            };

            await _investmentRepository.AddAsync(newInvestment);
            return newInvestment;
        }

        public async Task<IEnumerable<Investment>> GetUserPortfolioAsync(int userId)
        {
            return await _investmentRepository.GetInvestmentsByUserIdAsync(userId);
        }
    }
}
