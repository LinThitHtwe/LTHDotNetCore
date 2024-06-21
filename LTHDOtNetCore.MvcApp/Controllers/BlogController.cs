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
            var blogs = await _context.Blogs.OrderByDescending(blog => blog.Id).ToListAsync();
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

        [ActionName("Edit")]
        public async Task<IActionResult> BlogEdit(int id)
        {
            var blog = await _context.Blogs.FirstOrDefaultAsync(b=> b.Id == id);
            if(blog is null)
            {
                return Redirect("/Error");
            }
            return View("BlogEdit",blog);
        }

        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> BlogEdit(int id, BlogModel requestBlog)
        {
            var blog = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == id);
            if (blog is null)
            {
                return Redirect("/Error");
            }

            blog.Title = requestBlog.Title;
            blog.Author = requestBlog.Author;
            blog.Content = requestBlog.Content;

            var result = await _context.SaveChangesAsync();
            if (result < 1)
            {
                return Redirect("/Error");
            }

            return Redirect("/Blog");
        }
    }
}
