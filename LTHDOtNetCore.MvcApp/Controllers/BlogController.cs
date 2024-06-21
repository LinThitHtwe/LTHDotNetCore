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

        [ActionName("Create")]
        public IActionResult BlogCreate()
        {
            return View("BlogCreate");
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> BlogCreate(BlogModel requestBlog)
        {
            if (requestBlog == null)
            {
                return Redirect("/Error");
            }
            await _context.Blogs.AddAsync(requestBlog);
            var result = await _context.SaveChangesAsync();
            
            if(result < 1)
            {
                return Redirect("/Error");
            }

            return Redirect("/Blog");
        }

    }
}
