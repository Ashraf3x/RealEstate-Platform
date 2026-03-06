using RealEstate.Domain.Entities;

namespace RealEstate.Domain.Interfaces
{
    public interface IPropertyRepository : IGenericRepository<Property>
    {
        List<Property> GetPropertiesByStatus(string status);
        List<Property> GetPropertiesWithDocuments();
    }
}