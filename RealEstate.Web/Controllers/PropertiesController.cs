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
            if (property == null)
                return NotFound();
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
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
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var property = service.GetById(id);
            if (property == null)
                return NotFound();
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

        [HttpPost]
        public IActionResult Edit(Property property)
        {
            service.Update(property);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var property = service.GetById(id);
            if (property == null)
                return NotFound();
            return View(property);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var property = service.GetById(id);
            service.Delete(property);
            return RedirectToAction("Index");
        }
    }
}