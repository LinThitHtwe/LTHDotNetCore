using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LTHDOtNetCore.MvcChartApp.Controllers
{
    public class ChartJsController : Controller
    {
        public IActionResult ExampleChart()
        {
            return View();
        }

        public IActionResult InterpolationLineChart()
        {
            return View();
        }
    }
}
