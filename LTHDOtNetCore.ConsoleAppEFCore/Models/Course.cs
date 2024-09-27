using System;
using System.Collections.Generic;

namespace LTHDOtNetCore.ConsoleAppEFCore.Models;

public partial class Course
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
