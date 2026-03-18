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

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Submit(string documentType, string filePath)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");
            service.Submit(user.Id, documentType, filePath);
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
        public IActionResult Reject(int id)
        {
            service.Reject(id);
            return RedirectToAction("AdminIndex");
        }
    }
}