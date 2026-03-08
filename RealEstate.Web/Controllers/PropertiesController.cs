using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.DTOs;
using RealEstate.Application.Services;
using RealEstate.Domain.Entities;

namespace RealEstate.Web.Controllers
{
    public class PropertiesController : Controller
    {
        PropertyService service;

        public PropertiesController(PropertyService propertyService)
        {
            service = propertyService;
        }

        public IActionResult Index()
        {
            var properties = service.GetAll();
            var result = properties.Select(p => new PropertyDto
            {
                PropertyId = p.PropertyId,
                Title = p.Title,
                Description = p.Description,
                Location = p.Location,
                TotalShares = p.TotalShares,
                PricePerShare = p.PricePerShare,
                Status = p.Status,
                CreatedAt = p.CreatedAt
            }).ToList();
            return View(result);
        }

        public IActionResult Details(int id)
        {
            var property = service.GetById(id);
            if (property == null) return NotFound();
            var result = new PropertyDto
            {
                PropertyId = property.PropertyId,
                Title = property.Title,
                Description = property.Description,
                Location = property.Location,
                TotalShares = property.TotalShares,
                PricePerShare = property.PricePerShare,
                Status = property.Status,
                CreatedAt = property.CreatedAt
            };
            return View(result);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminIndex()
        {
            var properties = service.GetAll();
            var result = properties.Select(p => new PropertyDto
            {
                PropertyId = p.PropertyId,
                Title = p.Title,
                Description = p.Description,
                Location = p.Location,
                TotalShares = p.TotalShares,
                PricePerShare = p.PricePerShare,
                Status = p.Status,
                CreatedAt = p.CreatedAt
            }).ToList();
            return View("../Admin/Properties/Index", result);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View("../Admin/Properties/Create");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(CreatePropertyDto dto)
        {
            var property = new Property
            {
                Title = dto.Title,
                Description = dto.Description,
                Location = dto.Location,
                TotalShares = dto.TotalShares,
                PricePerShare = dto.PricePerShare,
                Status = dto.Status,
                CreatedAt = DateTime.Now
            };
            service.Add(property);
            return RedirectToAction("AdminIndex");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var property = service.GetById(id);
            if (property == null) return NotFound();
            var result = new PropertyDto
            {
                PropertyId = property.PropertyId,
                Title = property.Title,
                Description = property.Description,
                Location = property.Location,
                TotalShares = property.TotalShares,
                PricePerShare = property.PricePerShare,
                Status = property.Status,
                CreatedAt = property.CreatedAt
            };
            return View("../Admin/Properties/Edit", result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(PropertyDto dto)
        {
            var property = service.GetById(dto.PropertyId);
            property.Title = dto.Title;
            property.Description = dto.Description;
            property.Location = dto.Location;
            property.TotalShares = dto.TotalShares;
            property.PricePerShare = dto.PricePerShare;
            property.Status = dto.Status;
            service.Update(property);
            return RedirectToAction("AdminIndex");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var property = service.GetById(id);
            if (property == null) return NotFound();
            var result = new PropertyDto
            {
                PropertyId = property.PropertyId,
                Title = property.Title,
                Location = property.Location,
                Status = property.Status,
                PricePerShare = property.PricePerShare,
                TotalShares = property.TotalShares
            };
            return View("../Admin/Properties/Delete", result);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            var property = service.GetById(id);
            service.Delete(property);
            return RedirectToAction("AdminIndex");
        }
    }
}