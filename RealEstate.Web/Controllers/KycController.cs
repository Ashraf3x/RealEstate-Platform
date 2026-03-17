using Microsoft.AspNetCore.Mvc;

namespace RealEstate.Web.Controllers
{
    public class KycController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}