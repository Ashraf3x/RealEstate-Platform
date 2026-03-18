using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Infrastructure.Repositories
{
    public class KycRepository : GenericRepository<KycDocument>, IKycRepository
    {
        AppDbContext context;

        public KycRepository(AppDbContext ctx) : base(ctx)
        {
            context = ctx;
        }

        public List<KycDocument> GetByUserId(int userId)
        {
            return context.KycDocuments.Where(k => k.UserId == userId).ToList();
        }

        public List<KycDocument> GetAllWithUsers()
        {
            return context.KycDocuments.Include(k => k.User).ToList();
        }
    }
}