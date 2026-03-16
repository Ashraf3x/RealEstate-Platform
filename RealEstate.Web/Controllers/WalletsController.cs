using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.DTOs;
using RealEstate.Application.Services;
using System.Security.Claims;

namespace RealEstate.Web.Controllers
{
    [Authorize]
    public class WalletsController : Controller
    {
        WalletService walletService;

        public WalletsController(WalletService walletService)
        {
            this.walletService = walletService;
        }
        public IActionResult Index()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var wallet = walletService.GetWalletByUserId(userId);
            if (wallet == null) {
                return NotFound();
            };

            var result = new WalletDto
            {
                WalletId = wallet.WalletId,
                Balance = wallet.Balance,
            };

            return View(result);
        }

        public IActionResult Details(int id)
        {
            var wallet = walletService.GetById(id);
            if (wallet == null)
                return NotFound();
            var result = new WalletDto
            {
                WalletId = wallet.WalletId,
                UserId = wallet.UserId,
                Balance = wallet.Balance,
                CreatedAt = wallet.CreatedAt
            };
            return View(result);
        }

        public IActionResult Deposit(int walletId)
        {
            var model = new DepositDto { WalletId = walletId };
            return View(model);
        }

        [HttpPost]
        public IActionResult Deposit(DepositDto model)
        {
            if (model.Amount <= 0) {
                return BadRequest("Amount must be greater than zero");
            }
            walletService.Deposit(model.WalletId, model.Amount);
            return RedirectToAction("Index");
        }

        public IActionResult Withdraw(int walletId)
        {
            var model = new WithdrawDto { WalletId = walletId };
            return View(model);
        }

        [HttpPost]
        public IActionResult Withdraw(WithdrawDto model) {
            if (model.Amount <= 0) {
                return BadRequest("Amount must be greater zero");
            }
            walletService.Withdraw(model.WalletId, model.Amount);
            return RedirectToAction("Index");
        }

        public IActionResult GetTransactions(int walletId)
        {
            var transactions = walletService.GetTransactionsByWalletId(walletId);
            var result = transactions.Select(t => new WalletTransactionDto
            {
                TransactionId = t.TransactionId,
                WalletId = t.WalletId,
                Type = t.Type,
                Amount = t.Amount,
                Timestamp = t.Timestamp
            }).ToList();
            return View(result);
        }

        [Route("Admin/Wallets/Index")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllWallets()
        {
            var wallets = walletService.GetAll();
            var result = wallets.Select(w => new WalletDto
            {
                WalletId = w.WalletId,
                UserId = w.UserId,
                Balance = w.Balance,
                CreatedAt = w.CreatedAt
            }).ToList();
            return View("~/Views/Admin/Wallets/Index.cshtml", result);
        }

        [Route("Admin/Wallets/Transactions")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllTransactions()
        {
            var transactions = walletService.GetAllTransactions();
            var result = transactions.Select(t => new WalletTransactionDto
            {
                TransactionId = t.TransactionId,
                WalletId = t.WalletId,
                Type = t.Type,
                Amount = t.Amount,
                Timestamp = t.Timestamp
            }).ToList();
            return View("~/Views/Admin/Wallets/Transactions.cshtml", result);
        }
    }
}
