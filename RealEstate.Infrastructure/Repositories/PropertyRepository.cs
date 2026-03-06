using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Infrastructure.Repositories
{
    public class PropertyRepository : GenericRepository<Property>, IPropertyRepository
    {
        AppDbContext context;

        public PropertyRepository(AppDbContext con) : base(con)
        {
            context = con;
        }

        public new List<Property> GetAll()
        {
            return context.Properties.Include(p => p.PropertyDocuments).ToList();
        }

        public List<Property> GetPropertiesByStatus(string status)
        {
            return context.Properties.Where(p => p.Status == status).Include(p => p.PropertyDocuments).ToList();
        }

        public List<Property> GetPropertiesWithDocuments()
        {
            return context.Properties.Include(p => p.PropertyDocuments).ToList();
        }
    }
}