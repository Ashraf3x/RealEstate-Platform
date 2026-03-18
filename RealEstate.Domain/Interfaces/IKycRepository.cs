using RealEstate.Domain.Entities;

namespace RealEstate.Domain.Interfaces
{
    public interface IKycRepository : IGenericRepository<KycDocument>
    {
        List<KycDocument> GetByUserId(int userId);
        List<KycDocument> GetAllWithUsers();
    }
}