using Dapper;
using LTHDOtNetCore.RestApi.Db;
using LTHDOtNetCore.RestApi.Helpers;
using LTHDOtNetCore.RestApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using static LTHDOtNetCore.RestApi.Enums.Enum;

namespace LTHDOtNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapperController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllBlogs()
        {
            string query = "select * from blog";
            using IDbConnection db = new SqlConnection(ConnectionString.sqlConnectionStringBuilder.ConnectionString);
            List<BlogDapperModel> blogs = db.Query<BlogDapperModel>(query).ToList();
            return Ok(blogs);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlogById(int id)
        {
            var blog = FindById(id);
            if (blog is null)
            {
                return NotFound("No data found.");
            }
            return Ok(blog);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogDapperModel blog)
        {
            string query = @"INSERT INTO [dbo].[blog]
                            ([title],[author],[blogContent])
                            VALUES
                            (@title,@author,@blogContent)";

            using IDbConnection db = new SqlConnection(ConnectionString.sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);

            string responseMessage = ReturnMessages.ManipulatedStatusMessage(result, ManipulationMethods.create);
            return Ok(responseMessage);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogDapperModel blog)
        {
            var existingBlog = FindById(id);
            if (existingBlog is null)
            {
                return NotFound("No data found.");
            }

            blog.Id = id;
            string query = @"Update [dbo].[blog]
                            SET [title] = @title,
                                [author]=@author,
                                [blogContent]=@blogContent
                            WHERE id = @id";

            using IDbConnection db = new SqlConnection(ConnectionString.sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);

            string responseMessage = ReturnMessages.ManipulatedStatusMessage(result, ManipulationMethods.delete);
            return Ok(responseMessage);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogDapperModel blog)
        {
            var existingBlog = FindById(id);
            if (existingBlog is null)
            {
                return NotFound("No blog found.");
            }

            string conditions = string.Empty;
            if (!string.IsNullOrEmpty(blog.Title))
            {
                conditions += " [title] = @title, ";
            }
            if (!string.IsNullOrEmpty(blog.Author))
            {
                conditions += " [author] = @author, ";
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                conditions += " [blogContent] = @blogContent, ";
            }

            if (conditions.Length == 0)
            {
                return NotFound("No data to update.");
            }

            conditions = conditions.Substring(0, conditions.Length - 2);
            blog.Id = id;

            string query = $@"UPDATE [dbo].[blog]
                              SET {conditions} 
                              WHERE id = @id";

            using IDbConnection db = new SqlConnection(ConnectionString.sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);

            string responseMessage = ReturnMessages.ManipulatedStatusMessage(result, ManipulationMethods.delete);
            return Ok(responseMessage);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var isBlogExist = FindById(id);
            if (isBlogExist is null)
            {
                return NotFound("No blog found.");
            }

            string query = @"Delete From [dbo].[blog] WHERE id = @id";
            using IDbConnection db = new SqlConnection(ConnectionString.sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, new BlogModel { Id = id });

            string responseMessage = ReturnMessages.ManipulatedStatusMessage(result, ManipulationMethods.delete);
            return Ok(responseMessage);
        }



        private static BlogDapperModel? FindById(int id)
        {
            string query = "select * from blog where id = @id";
            using IDbConnection db = new SqlConnection(ConnectionString.sqlConnectionStringBuilder.ConnectionString);
            var blog = db.Query<BlogDapperModel>(query, new BlogDapperModel { Id = id }).FirstOrDefault();
            return blog;
        }
    }
}
