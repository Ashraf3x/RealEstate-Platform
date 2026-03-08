using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RealEstate.Application.DTOs;
using RealEstate.Application.Services;
using RealEstate.Domain.Entities;

namespace RealEstate.Web.Controllers
{
    public class SaleListingsController : Controller
    {
        SaleListingService service;
        PropertyService property;
        public SaleListingsController(SaleListingService saleListingService, PropertyService propertyService)
        {
            service = saleListingService;
            property = propertyService;
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
            var properties = property.GetAll();
            ViewBag.Properties = new SelectList(properties, "PropertyId", "Title");
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
            if (listing == null) return NotFound();
            var result = new SaleListingDto
            {
                ListingId = listing.ListingId,
                PropertyId = listing.PropertyId,
                PropertyTitle = listing.Property?.Title ?? "",
                PricePerShare = listing.PricePerShare,
                Quantity = listing.Quantity,
                Status = listing.Status
            };
            return View("~/Views/Admin/SaleListings/Edit.cshtml", result);
        }

        [HttpPost]
        public IActionResult Edit(SaleListingDto dto)
        {
            var listing = service.GetById(dto.ListingId);
            listing.PricePerShare = dto.PricePerShare;
            listing.Quantity = dto.Quantity;
            listing.Status = dto.Status;
            service.Update(listing);
            return RedirectToAction("AdminIndex");
        }

        public IActionResult Delete(int id)
        {
            var listing = service.GetById(id);
            if (listing == null) return NotFound();
            var result = new SaleListingDto
            {
                ListingId = listing.ListingId,
                PropertyTitle = listing.Property?.Title ?? "",
                PricePerShare = listing.PricePerShare,
                Quantity = listing.Quantity,
                Status = listing.Status
            };
            return View("~/Views/Admin/SaleListings/Delete.cshtml", result);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var listing = service.GetById(id);
            service.Delete(listing);
            return RedirectToAction("AdminIndex");
        }

        public IActionResult GetByPropertyId(int propertyId)
        {
            var listings = service.GetByPropertyId(propertyId);
            var result = listings.Select(s => new SaleListingDto
            {
                ListingId = s.ListingId,
                PropertyId = s.PropertyId,
                PropertyTitle = s.Property?.Title ?? "",
                PricePerShare = s.PricePerShare,
                Quantity = s.Quantity,
                Status = s.Status
            }).ToList();
            return View("Index", result);
        }

        public IActionResult GetByStatus(string status)
        {
            var listings = service.GetByStatus(status);
            var result = listings.Select(s => new SaleListingDto
            {
                ListingId = s.ListingId,
                PropertyId = s.PropertyId,
                PropertyTitle = s.Property?.Title ?? "",
                PricePerShare = s.PricePerShare,
                Quantity = s.Quantity,
                Status = s.Status
            }).ToList();
            return View("Index", result);
        }
        public IActionResult AdminIndex()
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
            return View("~/Views/Admin/SaleListings/Index.cshtml", result);
        }
    }
}