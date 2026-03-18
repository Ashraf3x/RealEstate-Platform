using Microsoft.AspNetCore.Identity;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Services
{
    public class KycService
    {
        IKycRepository repository;
        UserManager<User> userManager;
        public KycService(IKycRepository repo, UserManager<User> um)
        {
            repository = repo;
            userManager = um;
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
                Status = KycStatus.Pending,
                UploadedAt = DateTime.Now
            };
            repository.Add(doc);
        }

        public void Approve(int id)
        {
            var doc = repository.GetById(id);
            if (doc == null) return;

            doc.Status = KycStatus.Approved;
            doc.ProcessedAt = DateTime.Now;
            doc.RejectionReason = null;

            repository.Update(doc);

            var user = userManager.FindByIdAsync(doc.UserId.ToString()).Result;
            if (user != null)
            {
                user.IsVerified = true;
                userManager.UpdateAsync(user).Wait();
            }
        }

        public void Reject(int id, string reason)
        {
            var doc = repository.GetById(id);
            if (doc == null) return;

            doc.Status = KycStatus.Rejected;
            doc.ProcessedAt = DateTime.Now;
            doc.RejectionReason = reason;

            repository.Update(doc);
        }
    }
}