using Microsoft.AspNetCore.Mvc;

namespace RealEstate.Web.Controllers
{
    public class FinancialController : Controller
    {
        public IActionResult AdminIndex()
        {
            return View();
        }
    }
}