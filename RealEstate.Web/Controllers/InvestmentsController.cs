using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RealEstate.Application.DTOs;
using RealEstate.Application.Services;
using RealEstate.Domain.Entities;
using System.Security.Claims;

namespace RealEstate.Web.Controllers
{
    public class InvestmentsController : Controller
    {
        InvestmentService _investmentService;
        PropertyService _propertyService;
        public InvestmentsController(InvestmentService investmentService, PropertyService propertyService)
        {
            this._investmentService = investmentService;
            this._propertyService = propertyService;
        }
        public IActionResult Index()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

         var investmentsDtos = _investmentService.GetAll().Where(i => i.UserId == userId).Select(i => new InvestmentDto
        {
                InvestmentId = i.InvestmentId,
                UserName = i.User.FirstName + " " + i.User.LastName,
                ShareCount = i.ShareCount,
                OwnershipPercentage = i.OwnershipPercentage,
                PropertyName = i.Property.Title,
                PurchasedAt = i.PurchasedAt,
                AnnualYield = i.Property.AnnualYield ?? 0,
                OccupancyRate = i.Property.OccupancyRate ?? 0,
                AppreciationStatus = i.Property.AppreciationStatus ?? "Stable",
                AppreciationProgress = i.Property.AppreciationProgress ?? 0
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
                AnnualYield = investment.Property.AnnualYield ?? 0,
                OccupancyRate = investment.Property.OccupancyRate ?? 0,
                AppreciationStatus = investment.Property.AppreciationStatus ?? "Stable",
                AppreciationProgress = investment.Property.AppreciationProgress ?? 0
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
                var property = _propertyService.GetById(createInvestmentDto.PropertyId);

                if (property != null)
                {
                    var investment = new Investment
                    {
                        UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                        PropertyId = createInvestmentDto.PropertyId,
                        ShareCount = createInvestmentDto.ShareCount,
                        PurchasedAt = DateTime.UtcNow,
                        OwnershipPercentage = ((decimal)createInvestmentDto.ShareCount / property.TotalShares) * 100
                    };

                    _investmentService.Add(investment);
                    return RedirectToAction("Index");
                }
            }

            var properties = _propertyService.GetAll().ToList();
            ViewBag.Properties = new SelectList(properties, "PropertyId", "Title");
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

            ModelState.Remove("UserName");
            ModelState.Remove("PropertyName");
            ModelState.Remove("AppreciationStatus");
            if (ModelState.IsValid) 
            {
                var investment = _investmentService.GetById(id);
                if (investment == null)
                {
                    return NotFound();
                }
                investment.ShareCount = investmentDto.ShareCount;

                if (investment.Property != null && investment.Property.TotalShares > 0)
                {
                    investment.OwnershipPercentage = ((decimal)investmentDto.ShareCount / investment.Property.TotalShares) * 100;
                }
                else
                {
                    investment.OwnershipPercentage = ((decimal)investmentDto.ShareCount / 1000m) * 100;
                }

                _investmentService.Update(investment);

                return RedirectToAction("AdminIndex");
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var err in errors) { Console.WriteLine(err.ErrorMessage); }

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
        [HttpGet]
        public IActionResult ConfirmInvestment(int propertyId, int shares)
        {
            var property = _propertyService.GetById(propertyId);
            if (property == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) != null
                         ? int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))
                         : 1;

            var investment = new Investment
            {
                UserId = userId,
                PropertyId = propertyId,
                ShareCount = shares,
                PurchasedAt = DateTime.UtcNow,
                OwnershipPercentage = ((decimal)shares / property.TotalShares) * 100
            };

            _investmentService.Add(investment);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult SellShares(int id)
        {
            var investment = _investmentService.GetById(id);
            if (investment == null) return NotFound();

            var dto = new InvestmentDto
            {
                InvestmentId = investment.InvestmentId,
                PropertyName = investment.Property.Title,
                ShareCount = investment.ShareCount,
                OwnershipPercentage = investment.OwnershipPercentage,
                PurchasedAt = investment.PurchasedAt,
                UserName = investment.User.FirstName + " " + investment.User.LastName
            };
            return View(dto);
        }

        [HttpPost]
        public IActionResult SellShares(int id, int sharesToSell)
        {
            var investment = _investmentService.GetById(id);
            if (investment == null) return NotFound();

            if (sharesToSell <= 0 || sharesToSell > investment.ShareCount)
            {
                TempData["Error"] = "Invalid number of shares.";
                return RedirectToAction("SellShares", new { id });
            }

            if (sharesToSell == investment.ShareCount)
            {
                _investmentService.Delete(investment);
            }
            else
            {
                investment.ShareCount -= sharesToSell;
                investment.OwnershipPercentage = ((decimal)investment.ShareCount / investment.Property.TotalShares) * 100;
                _investmentService.Update(investment);
            }

            return RedirectToAction("Index");
        }
    }
}
