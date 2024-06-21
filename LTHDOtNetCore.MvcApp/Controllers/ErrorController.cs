using Microsoft.AspNetCore.Mvc;

namespace LTHDOtNetCore.MvcApp.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
