using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.DTOs;
using RealEstate.Application.Services;
using RealEstate.Domain.Entities;

namespace RealEstate.Web.Controllers
{
    public class UsersController : Controller
    {
        UserService service;

        public UsersController(UserService userService)
        {
            service = userService;
        }

        public IActionResult Index()
        {
            var users = service.GetAll();
            var result = users.Select(u => new UserDto
            {
                UserId = u.UserId,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Role = u.Role,
                IsActive = u.IsActive,
                CreatedAt = u.CreatedAt
            }).ToList();
            return View(result);
        }

        public IActionResult Details(int id)
        {
            var user = service.GetById(id);
            if (user == null) return NotFound();
            var result = new UserDto
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            };
            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateUserDto dto)
        {
            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Password = dto.Password,
                Role = dto.Role,
                IsActive = true,
                CreatedAt = DateTime.Now
            };
            service.Add(user);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var user = service.GetById(id);
            if (user == null) return NotFound();
            var result = new UserDto
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            };
            return View(result);
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            service.Update(user);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var user = service.GetById(id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var user = service.GetById(id);
            service.Delete(user);
            return RedirectToAction("Index");
        }
    }
}