using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Services
{
    public class UserService
    {
        IUserRepository repo;

        public UserService(IUserRepository userRepo)
        {
            repo = userRepo;
        }

        public List<User> GetAll()
        {
            return repo.GetAll();
        }

        public User GetById(int id)
        {
            return repo.GetById(id);
        }

        public void Add(User user)
        {
            repo.Add(user);
        }

        public void Update(User user)
        {
            repo.Update(user);
        }

        public void Delete(User user)
        {
            repo.Delete(user);
        }

        public User GetByEmail(string email)
        {
            return repo.GetByEmail(email);
        }

        public List<User> GetActiveUsers()
        {
            return repo.GetActiveUsers();
        }
    }
}