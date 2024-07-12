using Microsoft.AspNetCore.Mvc;

namespace LTHDOtNetCore.MvcChartApp.Controllers
{
    public class HighChartsController : Controller
    {
        public IActionResult PieChart()
        {
            return View();
        }
    }
}
