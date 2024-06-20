using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LTHDOtNetCore.MvcApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var blogs = await _context.Blogs.ToListAsync();
            return View(blogs);
        }
    }
}
