using System;
using System.Collections.Generic;

namespace LTHDOtNetCore.ConsoleAppEFCore.Models;

public partial class PieChart
{
    public int PieChartId { get; set; }

    public string? PieChartName { get; set; }

    public decimal? PieChartValue { get; set; }
}
