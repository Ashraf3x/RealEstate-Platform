using RealEstate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Domain.Interfaces
{
    public interface IInvestmentRepository
    {
        Task<IEnumerable<Investment>> GetAllAsync();
        Task<Investment> GetByIdAsync(int id);
        Task<IEnumerable<Investment>> GetInvestmentsByUserIdAsync(int userId);
        Task<IEnumerable<Investment>> GetInvestmentsByPropertyIdAsync(int propertyId);
        Task AddAsync(Investment investment);
        void Update(Investment investment);
        void Delete(Investment investment);
    }
}
