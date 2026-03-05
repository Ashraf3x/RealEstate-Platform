using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Services
{
    public class InvestmentService
    {
        IInvestmentRepository repo;

        public InvestmentService(IInvestmentRepository investmentRepo)
        {
            repo = investmentRepo;
        }

        public List<Investment> GetAll()
        {
            return repo.GetAll();
        }

        public Investment GetById(int id)
        {
            return repo.GetById(id);
        }

        public void Add(Investment investment)
        {
            repo.Add(investment);
        }

        public void Update(Investment investment)
        {
            repo.Update(investment);
        }

        public void Delete(Investment investment)
        {
            repo.Delete(investment);
        }

        public List<Investment> GetByUserId(int userId)
        {
            return repo.GetInvestmentsByUserId(userId);
        }

        public List<Investment> GetByPropertyId(int propertyId)
        {
            return repo.GetInvestmentsByPropertyId(propertyId);
        }
    }
}