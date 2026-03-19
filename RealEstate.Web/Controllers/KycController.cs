using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Services;
using RealEstate.Domain.Entities;

namespace RealEstate.Web.Controllers
{
    [Authorize]
    public class KycController : Controller
    {
         KycService service;
        UserManager<User> userManager;

        public KycController(KycService kycService, UserManager<User> um)
        {
            service = kycService;
            userManager = um;
        }

        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            var userDocs = service.GetByUserId(user.Id);
            return View(userDocs);
        }

        [HttpPost]
        public async Task<IActionResult> Submit(string documentType, IFormFile documentFile)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            if (documentFile != null && documentFile.Length > 0)
            {
                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "kyc");
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(documentFile.FileName);
                var path = Path.Combine(folder, fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await documentFile.CopyToAsync(fileStream);
                }

                service.Submit(user.Id, documentType, "/uploads/kyc/" + fileName);
                TempData["Success"] = "Document uploaded successfully.";
            }
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult AdminIndex()
        {
            var docs = service.GetAll();
            return View("~/Views/Admin/Kyc/Index.cshtml", docs);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Approve(int id)
        {
            service.Approve(id);
            return RedirectToAction("AdminIndex");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Reject(int id, string reason)
        {
            service.Reject(id, reason);
            return RedirectToAction("AdminIndex");
        }
    }
}