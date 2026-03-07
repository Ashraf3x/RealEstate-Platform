using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.DTOs;
using RealEstate.Application.Services;
using RealEstate.Domain.Entities;

namespace RealEstate.Web.Controllers
{
    public class UsersController : Controller
    {
        UserService service;
        UserManager<User> manager;
        public UsersController(UserService userService, UserManager<User> userManager)
        {
            service = userService;
            manager = userManager;
        }

        public IActionResult Index()
        {
            var users = service.GetAll();
            var result = users.Select(u => new UserDto
            {
                UserId = u.Id,
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
                UserId = user.Id,
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
        public async Task<IActionResult> Create(CreateUserDto dto)
        {

            if (!ModelState.IsValid) return View(dto);

            var user = new User
            {
                UserName = dto.Email,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Role = dto.Role,
                IsActive = true,
                CreatedAt = DateTime.Now
            };

            var result = await manager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(dto.Role))
                {
                    await manager.AddToRoleAsync(user, dto.Role);
                }

                return RedirectToAction("Index");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(dto);
        }

        public IActionResult Edit(int id)
        {
            var user = service.GetById(id);
            if (user == null) return NotFound();
            var result = new UserDto
            {
                UserId = user.Id,
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