using Microsoft.AspNetCore.Mvc;

namespace RealEstate.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
