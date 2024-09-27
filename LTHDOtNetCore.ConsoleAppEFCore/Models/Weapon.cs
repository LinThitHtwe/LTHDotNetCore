using System;
using System.Collections.Generic;

namespace LTHDOtNetCore.ConsoleAppEFCore.Models;

public partial class Weapon
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CharacterId { get; set; }

    public virtual Character Character { get; set; } = null!;
}
