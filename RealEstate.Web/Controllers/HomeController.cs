using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.DTOs;
using RealEstate.Application.Services;
using System.Security.Claims;

namespace RealEstate.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly WalletService _walletService;
        private readonly PropertyService _propertyService;
        private readonly InvestmentService _investmentService;
        private readonly UserService _userService;

        public HomeController(WalletService walletService, PropertyService propertyService, InvestmentService investmentService, UserService userService)
        {
            _walletService = walletService;
            _propertyService = propertyService;
            _investmentService = investmentService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                var allUsers = _userService.GetAll();
                var allInvestments = _investmentService.GetAll();
                var allWallets = _walletService.GetAll();

                var model = new DashboardDto
                {
                    TotalUsers = allUsers.Count,
                    TotalProperties = _propertyService.GetAll().Count,
                    TotalPlatformInvestments = allInvestments.Sum(i => i.ShareCount * i.Property.PricePerShare),
                    TotalPlatformBalance = allWallets.Sum(w => w.Balance),
                    RecentUsers = allUsers
                        .OrderByDescending(u => u.CreatedAt)
                        .Take(4)
                        .Select(u => new UserDto
                        {
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            Email = u.Email,
                            Role = u.Role,
                            IsActive = u.IsActive,
                            CreatedAt = u.CreatedAt
                        }).ToList(),
                    RecentPlatformTransactions = _walletService.GetAllTransactions()
                        .OrderByDescending(t => t.Timestamp)
                        .Take(4)
                        .Select(t => new WalletTransactionDto
                        {
                            TransactionId = t.TransactionId,
                            WalletId = t.WalletId,
                            Type = t.Type,
                            Amount = t.Amount,
                            Timestamp = t.Timestamp
                        }).ToList()
                };

                return View(model);
            }
            else
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var wallet = _walletService.GetWalletByUserId(userId);
                var userInvestments = _investmentService.GetByUserId(userId);

                var model = new DashboardDto
                {
                    WalletId = wallet?.WalletId ?? 0,
                    WalletBalance = wallet?.Balance ?? 0,
                    TotalInvested = userInvestments.Sum(i => i.ShareCount * i.Property.PricePerShare),
                    ActiveInvestmentsCount = userInvestments.Count,
                    RecentTransactions = wallet == null ? new() : _walletService.GetTransactionsByWalletId(wallet.WalletId)
                        .OrderByDescending(t => t.Timestamp)
                        .Take(5)
                        .Select(t => new WalletTransactionDto
                        {
                            TransactionId = t.TransactionId,
                            WalletId = t.WalletId,
                            Type = t.Type,
                            Amount = t.Amount,
                            Timestamp = t.Timestamp
                        }).ToList(),
                    NewProperties = _propertyService.GetAll()
                        .OrderByDescending(p => p.CreatedAt)
                        .Take(3)
                        .Select(p => new PropertyDto
                        {
                            PropertyId = p.PropertyId,
                            Title = p.Title,
                            Description = p.Description,
                            Location = p.Location,
                            PricePerShare = p.PricePerShare,
                            Status = p.Status
                        }).ToList(),
                    UserInvestments = userInvestments
                        .Select(i => new InvestmentDto
                        {
                            InvestmentId = i.InvestmentId,
                            PropertyName = i.Property.Title,
                            ShareCount = i.ShareCount,
                            OwnershipPercentage = i.OwnershipPercentage,
                            PurchasedAt = i.PurchasedAt
                        }).ToList()
                };

                return View(model);
            }
        }
    }
}
