using LTHDOtNetCore.RealTimeChartApp.Hubs;
using LTHDOtNetCore.RealTimeChartApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LTHDOtNetCore.RealTimeChartApp.Controllers;

public class PieChartController : Controller
{
    private readonly AppDbContext _dbContext;
    private readonly IHubContext<ChartHub> _hubContext;

    public PieChartController(AppDbContext dbContext,IHubContext<ChartHub> hubContext)
    {
        _dbContext = dbContext;
        _hubContext = hubContext;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Add()
    {
        return View();
    }

    public async Task<IActionResult> Save(PieChart pieChart)
    {
        await _dbContext.PieCharts.AddAsync(pieChart);
        await _dbContext.SaveChangesAsync();

        var pieChartLists = await _dbContext.PieCharts.AsNoTracking().ToListAsync();
        var pieChartUiData = pieChartLists.Select(pieChartData => new PieChartDataModel()
        {
            name = pieChartData.PieChartName!,
            y = pieChartData.PieChartValue
        }).ToList();


        await _hubContext.Clients.All.SendAsync("ReceivePieChart", pieChartUiData);
        return RedirectToAction("Add");
    }
}

public class PieChartDataModel
{
    public string name { get; set; }
    public decimal y { get; set; }
}
