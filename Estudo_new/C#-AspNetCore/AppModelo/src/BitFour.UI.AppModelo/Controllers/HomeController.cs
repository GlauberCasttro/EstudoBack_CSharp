using Microsoft.AspNetCore.Mvc;

namespace BitFour.UI.AppModelo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
