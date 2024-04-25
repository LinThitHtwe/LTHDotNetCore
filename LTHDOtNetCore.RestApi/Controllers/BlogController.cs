using LTHDOtNetCore.RestApi.Db;
using LTHDOtNetCore.RestApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static LTHDOtNetCore.RestApi.Enums.Enum;

namespace LTHDOtNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private AppDbContext _dbContext;
        
        public BlogController()
        {
            _dbContext = new AppDbContext();
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<BlogModel>), 200)]
        public IActionResult GetBlogs()
        {
            var blogs = _dbContext.Blogs.ToList();
            return Ok(blogs);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BlogModel),200)]
        [ProducesResponseType(404)]
        public IActionResult GetBlogById(int id)
        {
            var blog = GetBlogByIdHelper(id);
            if(blog is null)
            {
                return NotFound();
            }

            return Ok(blog);
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        public IActionResult CreateBlog(BlogModel requestBlog)
        {
            if(requestBlog is null)
            {
                return BadRequest();
            }

            if(requestBlog.Title.Length < 2)
            {
                return BadRequest("Title length should be more than 2 characters");
            }

            _dbContext.Blogs.Add(requestBlog);
            int result = _dbContext.SaveChanges();
            string responseMessage = ManipulatedStatusMessage(result, ManipulationMethods.create);
            return Ok(responseMessage);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(424)]
        public IActionResult UpdateBlog(int id,BlogModel requestBlog)
        {
            if (requestBlog is null)
            {
                return BadRequest("Field Require");
            }
            if(requestBlog.Id != id)
            {
                return BadRequest("Different Ids in request");
            }
            var existingBlog = GetBlogByIdHelper(id);
            if (existingBlog is null)
            {
                return NotFound();
            }
            existingBlog.Title = requestBlog.Title;
            existingBlog.Author = requestBlog.Author;
            existingBlog.Content = requestBlog.Content;
            int result = _dbContext.SaveChanges();
            string responseMessage = ManipulatedStatusMessage(result, ManipulationMethods.delete);
            return Ok(responseMessage);
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(424)]
        public IActionResult DeleteBlog(int id)
        {
            var blog = GetBlogByIdHelper(id);
            if (blog is null)
            {
                return NotFound();
            }
            _dbContext.Blogs.Remove(blog);
            int result = _dbContext.SaveChanges();
            string responseMessage = ManipulatedStatusMessage(result, ManipulationMethods.delete);
            return Ok(responseMessage);
        }


        private BlogModel? GetBlogByIdHelper(int id)
        {
            var blog = _dbContext.Blogs.FirstOrDefault(blog => blog.Id == id);
            return blog;
        }

        private static string ManipulatedStatusMessage(int result, ManipulationMethods manipulationMethods)
        {
            string status = result > 0 ? "Successfully" : "Fail";

            return manipulationMethods switch
            {
                (ManipulationMethods.create) => ($"Create {status}"),
                (ManipulationMethods.update) => ($"Update {status}"),
                (ManipulationMethods.delete) => ($"Delete {status}"),
                _ => ("Something Went Wrong"),
            };
        }
    }
}
