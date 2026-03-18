using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RealEstate.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FinancialController : Controller
    {
        public IActionResult AdminIndex()
        {
            return RedirectToAction("GetAllWallets", "Wallets");
        }
    }
}