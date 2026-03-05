using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Services
{
    public class SaleListingService
    {
        ISaleListingRepository saleListingRepo;

        public SaleListingService(ISaleListingRepository repo)
        {
            saleListingRepo = repo;
        }

        public List<SaleListing> GetAll()
        {
            return saleListingRepo.GetAll();
        }

        public SaleListing GetById(int id)
        {
            return saleListingRepo.GetById(id);
        }

        public void Add(SaleListing listing)
        {
            saleListingRepo.Add(listing);
        }

        public void Update(SaleListing listing)
        {
            saleListingRepo.Update(listing);
        }

        public void Delete(SaleListing listing)
        {
            saleListingRepo.Delete(listing);
        }

        public List<SaleListing> GetByPropertyId(int propertyId)
        {
            return saleListingRepo.GetListingsByPropertyId(propertyId);
        }

        public List<SaleListing> GetByStatus(string status)
        {
            return saleListingRepo.GetListingsByStatus(status);
        }
    }
}