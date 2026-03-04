using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using RealEstate.Infrastructure.Data;
namespace RealEstate.Infrastructure.Repositories
{
    public class InvestmentRepository : IInvestmentRepository
    {
        private readonly AppDbContext _context;
        public InvestmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Investment>> GetAllAsync()
        {
            return await _context.Investments
                .Include(i => i.User)
                .Include(i => i.Property)
                .ToListAsync();
        }

        public async Task<Investment> GetByIdAsync(int id)
        {
            return await _context.Investments
                .Include(i => i.User)
                .Include(i => i.Property)
                .FirstOrDefaultAsync(i => i.InvestmentId == id);
        }

        public async Task<IEnumerable<Investment>> GetInvestmentsByUserIdAsync(int userId)
        {
            return await _context.Investments
                .Where(i => i.UserId == userId)
                .Include(i => i.Property)
                .ToListAsync();
        }

        public async Task<IEnumerable<Investment>> GetInvestmentsByPropertyIdAsync(int propertyId)
        {
            return await _context.Investments
                .Where(i => i.PropertyId == propertyId)
                .Include(i => i.User)
                .ToListAsync();
        }

        public async Task AddAsync(Investment investment)
        {
            await _context.Investments.AddAsync(investment);
            await _context.SaveChangesAsync();
        }

        public void Update(Investment investment)
        {
            _context.Investments.Update(investment);
            _context.SaveChanges();
        }

        public void Delete(Investment investment)
        {
            _context.Investments.Remove(investment);
            _context.SaveChanges();
        }
    }
}
