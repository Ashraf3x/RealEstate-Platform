using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Services
{
    public class KycService
    {
        private readonly IKycRepository repository;

        public KycService(IKycRepository repo)
        {
            repository = repo;
        }

        public List<KycDocument> GetAll()
        {
            return repository.GetAllWithUsers();
        }

        public List<KycDocument> GetByUserId(int userId)
        {
            return repository.GetByUserId(userId);
        }

        public void Submit(int userId, string documentType, string filePath)
        {
            var doc = new KycDocument
            {
                UserId = userId,
                DocumentType = documentType,
                FilePath = filePath,
                Status = "Pending",
                UploadedAt = DateTime.Now
            };
            repository.Add(doc);
        }

        public void Approve(int id)
        {
            var doc = repository.GetById(id);
            if (doc == null) return;
            doc.Status = "Approved";
            repository.Update(doc);
        }

        public void Reject(int id)
        {
            var doc = repository.GetById(id);
            if (doc == null) return;
            doc.Status = "Rejected";
            repository.Update(doc);
        }
    }
}