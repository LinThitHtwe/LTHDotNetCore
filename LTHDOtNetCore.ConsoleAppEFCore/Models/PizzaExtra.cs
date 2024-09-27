using System;
using System.Collections.Generic;

namespace LTHDOtNetCore.ConsoleAppEFCore.Models;

public partial class PizzaExtra
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }
}
