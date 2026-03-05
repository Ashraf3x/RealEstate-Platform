using RealEstate.Domain.Entities;

namespace RealEstate.Domain.Interfaces
{
    public interface IInvestmentRepository : IGenericRepository<Investment>
    {
        List<Investment> GetInvestmentsByUserId(int userId);
        List<Investment> GetInvestmentsByPropertyId(int propertyId);
    }
}