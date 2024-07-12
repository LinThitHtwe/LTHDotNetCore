using Microsoft.AspNetCore.Mvc;

namespace LTHDOtNetCore.MvcChartApp.Controllers
{
    public class CanvasChartController : Controller
    {
        public IActionResult SimpleLineChart()
        {
            return View();
        }
    }
}
