using Microsoft.AspNetCore.Mvc;

namespace RealEstate.Web.Controllers
{
    public class SettingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
