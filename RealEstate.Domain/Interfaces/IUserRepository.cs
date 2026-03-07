using RealEstate.Domain.Entities;

namespace RealEstate.Domain.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User GetByEmail(string email);
        List<User> GetActiveUsers();
    }
}