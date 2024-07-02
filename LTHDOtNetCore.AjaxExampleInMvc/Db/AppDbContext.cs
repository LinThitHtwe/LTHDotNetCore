using LTHDOtNetCore.AjaxExampleInMvc.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTHDOtNetCore.AjaxExampleInMvc.Db
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions options) : base(options) { }
        public DbSet<BlogModel> Blogs { get; set; }

    }
}
