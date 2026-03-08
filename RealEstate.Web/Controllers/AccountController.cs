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
            if (!ModelState.IsValid)
                return View(dto);
            var result = await signInManager.PasswordSignInAsync(dto.UserName, dto.Password, dto.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "SaleListings");
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
                return RedirectToAction("Index", "SaleListings");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);
            return View(dto);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}