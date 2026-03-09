using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RealEstate.Application.DTOs;
using RealEstate.Application.Services;
using RealEstate.Domain.Entities;

namespace RealEstate.Web.Controllers
{
    public class InvestmentsController : Controller
    {
        private readonly InvestmentService _investmentService;
        private readonly PropertyService _propertyService;
        public InvestmentsController(InvestmentService investmentService, PropertyService propertyService)
        {
            this._investmentService = investmentService;
            this._propertyService = propertyService;
        }
        public IActionResult Index()
        {
            var investmentsDtos = _investmentService.GetAll().Select(i => new InvestmentDto
            {
                InvestmentId = i.InvestmentId,
                UserName = i.User.FirstName + " " + i.User.LastName,
                ShareCount = i.ShareCount,
                OwnershipPercentage = i.OwnershipPercentage,
                PropertyName = i.Property.Title,
                PurchasedAt = i.PurchasedAt,

            }).ToList();
            return View(investmentsDtos);
        }
        public IActionResult Details(int id)
        {
            var investment = _investmentService.GetById(id);
            if (investment == null)
            {
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
        [HttpGet]
        public IActionResult Create()
        {
            var properties = _propertyService.GetAll().ToList();
            ViewBag.Properties = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(properties, "PropertyId", "Title");
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateInvestmentDto createInvestmentDto)
        {

            if (ModelState.IsValid)
            {
                var investment = new Investment
                {
                    UserId = createInvestmentDto.UserId,
                    PropertyId = createInvestmentDto.PropertyId,
                    ShareCount = createInvestmentDto.ShareCount,
                    PurchasedAt = DateTime.UtcNow,
                    OwnershipPercentage = (createInvestmentDto.ShareCount / 1000m) * 100
                    //OwnershipPercentage = (createInvestmentDto.ShareCount / (decimal)property.TotalShares) * 100

                };
                _investmentService.Add(investment);
                return RedirectToAction("Index");
            }

            return View(createInvestmentDto);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var investment = _investmentService.GetById(id);
            if (investment == null)
            {
                return NotFound();
            }
            var investmentDto = new InvestmentDto
            {
                InvestmentId = investment.InvestmentId,
                ShareCount = investment.ShareCount,
                OwnershipPercentage = investment.OwnershipPercentage,
                PurchasedAt = investment.PurchasedAt,
                UserName = investment.User.FirstName + " " + investment.User.LastName,
                PropertyName = investment.Property.Title
            };
            return View("~/Views/Admin/Investments/Edit.cshtml", investmentDto);
        }
        [HttpPost]
        public IActionResult Edit(int id, InvestmentDto investmentDto)
        {
            if (id != investmentDto.InvestmentId)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                var investment = _investmentService.GetById(id);
                if (investment == null)
                {
                    return NotFound();
                }
                investment.ShareCount = investmentDto.ShareCount;
                investment.OwnershipPercentage = (investmentDto.ShareCount / 1000m) * 100;
                _investmentService.Update(investment);
                return RedirectToAction("Index");
            }
            return View("~/Views/Admin/Investments/Edit.cshtml", investmentDto);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var investment = _investmentService.GetById(id);
            if (investment == null)
            {
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

            return View("~/Views/Admin/Investments/Delete.cshtml", investmentDto);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            var investment = _investmentService.GetById(id);
            if (investment == null)
            {
                return NotFound();
            }
            _investmentService.Delete(investment);
            return RedirectToAction("AdminIndex");
        }
        public IActionResult GetByUserId(int userId)
        {
            var investments = _investmentService.GetByUserId(userId);
            if (investments == null)
            {
                return NotFound();
            }
            var investmentsDtos = investments.Select(i => new InvestmentDto
            {
                InvestmentId = i.InvestmentId,
                UserName = i.User.FirstName + " " + i.User.LastName,
                ShareCount = i.ShareCount,
                OwnershipPercentage = i.OwnershipPercentage,
                PropertyName = i.Property.Title,
                PurchasedAt = i.PurchasedAt,
            }).ToList();
            return View(investmentsDtos);
        }
        public IActionResult GetByPropertyId(int propertyId)
        {
            var investments = _investmentService.GetByPropertyId(propertyId);
            if (investments == null)
            {
                return NotFound();
            }

            var investmentsDtos = investments.Select(i => new InvestmentDto
            {
                InvestmentId = i.InvestmentId,
                UserName = i.User.FirstName + " " + i.User.LastName,
                ShareCount = i.ShareCount,
                OwnershipPercentage = i.OwnershipPercentage,
                PropertyName = i.Property.Title,
                PurchasedAt = i.PurchasedAt,
            }).ToList();
            return View(investmentsDtos);
        }
        [HttpGet]
        public IActionResult AdminIndex()
        {
            var investments = _investmentService.GetAll().Select(i => new InvestmentDto
            {
                InvestmentId = i.InvestmentId,
                UserName = i.User.FirstName + " " + i.User.LastName,
                ShareCount = i.ShareCount,
                OwnershipPercentage = i.OwnershipPercentage,
                PropertyName = i.Property.Title,
                PurchasedAt = i.PurchasedAt,
            }).ToList();

            return View("~/Views/Admin/Investments/Index.cshtml", investments);
        }
    }
}
