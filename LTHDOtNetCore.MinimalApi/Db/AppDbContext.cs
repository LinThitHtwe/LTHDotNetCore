using LTHDOtNetCore.MinimalApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LTHDOtNetCore.MinimalApi
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        public DbSet<BlogModel> Blogs { get; set; }

    }
}
