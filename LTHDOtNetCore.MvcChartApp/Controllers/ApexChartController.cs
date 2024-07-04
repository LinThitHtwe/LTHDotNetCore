using LTHDOtNetCore.MvcChartApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace LTHDOtNetCore.MvcChartApp.Controllers
{
    public class ApexChartController : Controller
    {
        public IActionResult SimplePieChart()
        {
            PieChartModel model = new()
            {
                Lables = new List<string>() { "Pizza", "HotDog", "Ramen", "Fried Rice", "Steak" },
                Series = new List<int> { 20, 11, 5, 10, 3 }
            };

            return View(model);
        }
    }
}
