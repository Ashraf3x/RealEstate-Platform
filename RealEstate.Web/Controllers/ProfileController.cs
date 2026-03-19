using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.DTOs;
using RealEstate.Domain.Entities;

namespace RealEstate.Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        UserManager<User> userManager;

        public ProfileController(UserManager<User> um)
        {
            userManager = um;
        }

        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");
            var dto = new UserDto
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role,
                PhoneNumber = user.PhoneNumber,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                ProfilePicturePath = user.ProfilePicturePath
            };
            return View(dto);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateDetails(UserDto model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            var user = await userManager.GetUserAsync(User);
            if (user != null)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;

                await userManager.UpdateAsync(user);
                TempData["Success"] = "Profile updated successfully.";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.GetUserAsync(User);
            if (user != null)
            {
                var result = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    TempData["Success"] = "Password changed successfully.";
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
        [HttpPost]
public async Task<IActionResult> UpdateProfile(UserDto model)
{
    var user = await userManager.FindByIdAsync(model.UserId.ToString());
    if (user == null) return NotFound();

    user.FirstName = model.FirstName;
    user.LastName = model.LastName;
    user.PhoneNumber = model.PhoneNumber;

    if (model.ProfilePicture != null && model.ProfilePicture.Length > 0)
    {
        var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/profiles");
        if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ProfilePicture.FileName);
        var filePath = Path.Combine(folder, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await model.ProfilePicture.CopyToAsync(stream);
        }

        user.ProfilePicturePath = "/uploads/profiles/" + fileName;
    }

    await userManager.UpdateAsync(user);
    return RedirectToAction("Index");
}
    }
}