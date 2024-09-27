using System;
using System.Collections.Generic;

namespace LTHDOtNetCore.ConsoleAppEFCore.Models;

public partial class Blog
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Author { get; set; } = null!;

    public string BlogContent { get; set; } = null!;
}
