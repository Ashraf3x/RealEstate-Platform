using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Infrastructure.Repositories
{
    public class InvestmentRepository : GenericRepository<Investment>, IInvestmentRepository
    {
        AppDbContext context;

        public InvestmentRepository(AppDbContext con) : base(con)
        {
            context = con;
        }

        public List<Investment> GetInvestmentsByUserId(int userId)
        {
            return context.Investments
                .Where(i => i.UserId == userId)
                .Include(i => i.Property)
                .ToList();
        }

        public List<Investment> GetInvestmentsByPropertyId(int propertyId)
        {
            return context.Investments
                .Where(i => i.PropertyId == propertyId)
                .Include(i => i.User)
                .ToList();
        }
        public List<Investment> GetAllWithDetails()
        {
            return context.Investments
                .Include(i => i.User)
                .Include(i => i.Property)
                .ToList();
        }
        public Investment GetByIdWithDetails(int id)
        {
            return context.Investments
                .Include(i => i.User)
                .Include(i => i.Property)
                .FirstOrDefault(i => i.InvestmentId == id);
        }
    }
}