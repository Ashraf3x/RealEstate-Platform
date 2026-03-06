using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.DTOs;
using RealEstate.Application.Services;
using RealEstate.Domain.Entities;

namespace RealEstate.Web.Controllers
{
    public class SaleListingsController : Controller
    {
        SaleListingService service;

        public SaleListingsController(SaleListingService saleListingService)
        {
            service = saleListingService;
        }

        public IActionResult Index()
        {
            var listings = service.GetAll();
            var result = listings.Select(s => new SaleListingDto
            {
                ListingId = s.ListingId,
                PropertyId = s.PropertyId,
                PropertyTitle = s.Property?.Title ?? "",
                PricePerShare = s.PricePerShare,
                Quantity = s.Quantity,
                Status = s.Status
            }).ToList();
            return View(result);
        }

        public IActionResult Details(int id)
        {
            var listing = service.GetById(id);
            if (listing == null)
                return NotFound();
            var result = new SaleListingDto
            {
                ListingId = listing.ListingId,
                PropertyId = listing.PropertyId,
                PropertyTitle = listing.Property?.Title ?? "",
                PricePerShare = listing.PricePerShare,
                Quantity = listing.Quantity,
                Status = listing.Status
            };
            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateSaleListingDto dto)
        {
            var listing = new SaleListing
            {
                PropertyId = dto.PropertyId,
                PricePerShare = dto.PricePerShare,
                Quantity = dto.Quantity,
                Status = dto.Status
            };
            service.Add(listing);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var listing = service.GetById(id);
            if (listing == null)
                return NotFound();
            return View(listing);
        }

        [HttpPost]
        public IActionResult Edit(SaleListing listing)
        {
            service.Update(listing);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var listing = service.GetById(id);
            if (listing == null)
                return NotFound();
            return View(listing);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var listing = service.GetById(id);
            service.Delete(listing);
            return RedirectToAction("Index");
        }

        public IActionResult GetByPropertyId(int propertyId)
        {
            var listings = service.GetByPropertyId(propertyId);
            return View("Index", listings);
        }

        public IActionResult GetByStatus(string status)
        {
            var listings = service.GetByStatus(status);
            return View("Index", listings);
        }
    }
}