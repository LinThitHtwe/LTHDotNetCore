using LTHDOtNetCore.RestApiWithNLayer.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LTHDOtNetCore.RestApiWithNLayer.Enums;

namespace LTHDOtNetCore.RestApiWithNLayer.Features.Blog
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BusinessLogic_Blog _businessLogic_Blog;
        private readonly ReturnMessages _returnMessages;

        public BlogController()
        {
            _businessLogic_Blog = new BusinessLogic_Blog();
            _returnMessages = new ReturnMessages();
        }

        [HttpGet]
        public IActionResult GetAllBlogs()
        {
            var blogs = _businessLogic_Blog.GetAllBlogs();
            return Ok(blogs);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlogById(int id)
        {
            var blog = _businessLogic_Blog.GetBlogById(id);

            if (blog is null)
            {
                return NotFound("No blog found.");
            }

            return Ok(blog);
        }

        [HttpPost]
        public IActionResult Create(BlogModel requestBlog)
        {
            var result = _businessLogic_Blog.CreateBlog(requestBlog);

            string message = _returnMessages.ManipulatedStatusMessage(result, ManipulationMethods.create);
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogModel requestBlog)
        {
            var isBlogExist = _businessLogic_Blog.GetBlogById(id);
            if (isBlogExist is null)
            {
                return NotFound("No blog found.");
            }

            var result = _businessLogic_Blog.UpdateBlog(id, requestBlog);
            string message = _returnMessages.ManipulatedStatusMessage(result, ManipulationMethods.update);
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, BlogModel requestBlog)
        {
            var existingBlog = _businessLogic_Blog.GetBlogById(id);
            
            if (existingBlog is null)
            {
                return NotFound("No blog found.");
            }

            if (string.IsNullOrEmpty(requestBlog.Title))
            {
                requestBlog.Title = existingBlog.Title;
            }

            if (string.IsNullOrEmpty(requestBlog.Author))
            {
                requestBlog.Author = existingBlog.Author;
            }

            if (string.IsNullOrEmpty(requestBlog.Content))
            {
                requestBlog.Content = existingBlog.Content;
            }

            var result = _businessLogic_Blog.UpdateBlog(id,requestBlog);

            string message = _returnMessages.ManipulatedStatusMessage(result, ManipulationMethods.update);
            return Ok(message); ;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var isBlogExist = _businessLogic_Blog.GetBlogById(id);
            if (isBlogExist is null)
            {
                return NotFound("No blog found.");
            }

            var result = _businessLogic_Blog.DeleteBlog(id);
            string message = _returnMessages.ManipulatedStatusMessage(result, ManipulationMethods.delete);
            return Ok(message);
        }
    }
}
