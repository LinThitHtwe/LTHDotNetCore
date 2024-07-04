using LTHDOtNetCore.AjaxExampleInMvc.Db;
using LTHDOtNetCore.AjaxExampleInMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LTHDOtNetCore.AjaxExampleInMvc.Controllers
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
            var blogs = await _context.Blogs.AsNoTracking()
                                            .OrderByDescending(blog => blog.Id)
                                            .ToListAsync();
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

            if (result < 1)
            {
                return Redirect("/Error");
            }

            JsonResponseModel jsonResponse = new()
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Successfully Created" : "Create Failed.",
                Data = requestBlog
            };

            return Json(jsonResponse);
        }

        [ActionName("Edit")]
        public async Task<IActionResult> BlogEdit(int id)
        {
            var blog = await GetBlogById(id);
            if (blog is null)
            {
                return Redirect("/Error");
            }
            return View("BlogEdit", blog);
        }

        [HttpPut]
        [ActionName("Update")]
        public async Task<IActionResult> BlogEdit(int id, BlogModel requestBlog)
        {
            var blog = await GetBlogById(id);
            if (blog is null)
            {
                return Redirect("/Error");
            }

            blog.Title = requestBlog.Title;
            blog.Author = requestBlog.Author;
            blog.Content = requestBlog.Content;

            _context.Entry(blog).State = EntityState.Modified;

            var result = await _context.SaveChangesAsync();
            if (result < 1)
            {
                return Redirect("/Error");
            }

            JsonResponseModel jsonResponse = new()
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Successfully Updated" : "Update Failed.",
                Data = requestBlog
            };

            return Json(jsonResponse);
        }

        [ActionName("Delete")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var blog = await GetBlogById(id);
            if (blog is null)
            {
                return Redirect("/Error");
            }

            _context.Blogs.Remove(blog);
            var result = await _context.SaveChangesAsync();
            if (result < 1)
            {
                return Redirect("/Error");
            }

            JsonResponseModel jsonResponse = new()
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Successfully Deleted" : "Delete Failed.",
                Data = null
            };

            return Json(jsonResponse);
        }


        private async Task<BlogModel?> GetBlogById(int id)
        {
            var blog = await _context.Blogs.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
            return blog;
        }
    }
}
