using System;
using System.Collections.Generic;

namespace LTHDOtNetCore.ConsoleAppEFCore.Models;

public partial class Character
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual Backpack? Backpack { get; set; }

    public virtual ICollection<Weapon> Weapons { get; set; } = new List<Weapon>();

    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
}
