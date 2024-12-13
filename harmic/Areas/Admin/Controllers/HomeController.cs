using Microsoft.AspNetCore.Mvc;
using harmic.Utilities;
namespace harmic.Areas.admin.Controllers
{
    public class HomeController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            if (Function.account == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
    }
}
