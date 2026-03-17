using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.DTOs;
using RealEstate.Application.Services;
using RealEstate.Domain.Entities;

namespace RealEstate.Web.Controllers
{
    public class AccountController : Controller
    {
        UserManager<User> userManager;
        SignInManager<User> signInManager;
        // Wallet Creation in Account
        WalletService walletService;

        public AccountController(UserManager<User> user, SignInManager<User> sign, WalletService service)
        {
            userManager = user;
            signInManager = sign;
            walletService = service;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var result = await signInManager.PasswordSignInAsync(dto.UserName, dto.Password, dto.RememberMe, false);

            if (result.Succeeded)
            {
                var user = await userManager.FindByNameAsync(dto.UserName);
                if (user == null) return View(dto);
                var roles = await userManager.GetRolesAsync(user);
                var role = roles.FirstOrDefault() ?? "User";

                HttpContext.Session.SetString("Role", role);
                HttpContext.Session.SetString("UserName", user.UserName);
                HttpContext.Session.SetString("UserId", user.Id.ToString());

                if (role == "Admin")
                    return RedirectToAction("Index", "Home", new { area = "" });
                else
                    return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid username or password");
            return View(dto);
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            User user = new User
            {
                UserName = dto.UserName,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Role = "User",
                IsActive = true,
                CreatedAt = DateTime.Now
            };

            IdentityResult result = await userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
            {
                walletService.CreateWallet(user.Id);
                await userManager.AddToRoleAsync(user, "User");
                await signInManager.SignInAsync(user, false);
                HttpContext.Session.SetString("Role", "User");
                HttpContext.Session.SetString("UserName", user.UserName);
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);
            return View(dto);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}