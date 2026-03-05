using RealEstate.Domain.Entities;

namespace RealEstate.Domain.Interfaces
{
    public interface ISaleListingRepository : IGenericRepository<SaleListing>
    {
        List<SaleListing> GetListingsByPropertyId(int propertyId);
        List<SaleListing> GetListingsByStatus(string status);
    }
}