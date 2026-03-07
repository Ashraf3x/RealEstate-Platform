using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        AppDbContext context;

        public UserRepository(AppDbContext con) : base(con)
        {
            context = con;
        }

        public new List<User> GetAll()
        {
            return context.Users.Include(u => u.KycDocuments).Include(u => u.Wallet).ToList();
        }

        public User GetByEmail(string email)
        {
            return context.Users.FirstOrDefault(u => u.Email == email);
        }

        public List<User> GetActiveUsers()
        {
            return context.Users.Where(u => u.IsActive == true).ToList();
        }
    }
}