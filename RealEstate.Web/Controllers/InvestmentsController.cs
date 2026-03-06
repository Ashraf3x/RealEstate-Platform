using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.DTOs;
using RealEstate.Application.Services;

namespace RealEstate.Web.Controllers
{
    public class InvestmentsController : Controller
    {
        private readonly InvestmentService _investmentService;
        public InvestmentsController(InvestmentService investmentService) {
            this._investmentService = investmentService;
        }
        public IActionResult Index()
        {
            var investmentsDtos= _investmentService.GetAll().Select(i=>new InvestmentDto
            {
                InvestmentId=i.InvestmentId,
                UserName=i.User.FirstName+" "+i.User.LastName,
                ShareCount=i.ShareCount,
                OwnershipPercentage=i.OwnershipPercentage,
                PropertyName=i.Property.Title,
                PurchasedAt=i.PurchasedAt,

            }).ToList();
            return View(investmentsDtos);
        }
        public IActionResult Details(int id) { 
            var investment=_investmentService.GetById(id);
            if (investment == null) {
                return NotFound();
            }
            var investmentDto = new InvestmentDto
            {
                InvestmentId = investment.InvestmentId,
                UserName = investment.User.FirstName + " " + investment.User.LastName,
                ShareCount = investment.ShareCount,
                OwnershipPercentage = investment.OwnershipPercentage,
                PropertyName = investment.Property.Title,
                PurchasedAt = investment.PurchasedAt,
            };
            return View(investmentDto);
        }
    }
}
