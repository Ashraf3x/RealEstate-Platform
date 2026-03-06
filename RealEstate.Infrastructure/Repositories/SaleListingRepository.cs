using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Infrastructure.Repositories
{
    public class SaleListingRepository : GenericRepository<SaleListing>, ISaleListingRepository
    {
        AppDbContext context;

        public SaleListingRepository(AppDbContext con) : base(con)
        {
            context = con;
        }
        public List<SaleListing> GetAll()
        {
            return context.SaleListings
                .Include(s => s.Property)
                .ToList();
        }
        public List<SaleListing> GetListingsByPropertyId(int propertyId)
        {
            return context.SaleListings
                .Where(s => s.PropertyId == propertyId)
                .Include(s => s.Property)
                .ToList();
        }

        public List<SaleListing> GetListingsByStatus(string status)
        {
            return context.SaleListings
                .Where(s => s.Status == status)
                .Include(s => s.Property)
                .ToList();
        }
    }
}