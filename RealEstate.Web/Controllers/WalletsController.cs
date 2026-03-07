using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.DTOs;
using RealEstate.Application.Services;

namespace RealEstate.Web.Controllers
{
    public class WalletsController : Controller
    {
        WalletService walletService;

        public WalletsController(WalletService walletService)
        {
            this.walletService = walletService;
        }
        public IActionResult Index()
        {
            var userId = 1; // Placeholder until auth
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
            return View();
        }

        [HttpPost]
        public IActionResult Deposit(int walletId, decimal amount)
        {
            walletService.Deposit(walletId, amount);
            return RedirectToAction("Index");
        }

        public IActionResult Withdraw(int walletId)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Withdraw(int walletId, decimal amount) {
            walletService.Withdraw(walletId, amount);
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
    }
}
