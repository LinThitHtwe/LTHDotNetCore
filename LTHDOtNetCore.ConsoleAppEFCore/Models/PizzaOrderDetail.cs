using System;
using System.Collections.Generic;

namespace LTHDOtNetCore.ConsoleAppEFCore.Models;

public partial class PizzaOrderDetail
{
    public int Id { get; set; }

    public string OrderInvoiceNo { get; set; } = null!;

    public int PizzaExtraId { get; set; }
}
