using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Services
{
    public class PropertyService
    {
        IPropertyRepository propertyRepo;

        public PropertyService(IPropertyRepository repo)
        {
            propertyRepo = repo;
        }

        public List<Property> GetAll()
        {
            return propertyRepo.GetAll();
        }

        public Property GetById(int id)
        {
            return propertyRepo.GetById(id);
        }

        public void Add(Property property)
        {
            propertyRepo.Add(property);
        }

        public void Update(Property property)
        {
            propertyRepo.Update(property);
        }

        public void Delete(Property property)
        {
            propertyRepo.Delete(property);
        }

        public List<Property> GetByStatus(string status)
        {
            return propertyRepo.GetPropertiesByStatus(status);
        }

        public List<Property> GetWithDocuments()
        {
            return propertyRepo.GetPropertiesWithDocuments();
        }
    }
}