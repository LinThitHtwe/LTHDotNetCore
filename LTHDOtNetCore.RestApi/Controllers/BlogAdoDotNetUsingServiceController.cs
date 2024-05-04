using LTHDOtNetCore.RestApi.Db;
using LTHDOtNetCore.RestApi.DTOs;
using LTHDOtNetCore.RestApi.Helpers;
using LTHDOtNetCore.RestApi.Models;
using LTHDOtNetCore.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;
using System.Text;
using static LTHDOtNetCore.RestApi.Enums.Enum;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
namespace LTHDOtNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNetUsingServiceController : ControllerBase
    {
        private readonly AdoDotNetService _adoDotNetService = new(ConnectionString.sqlConnectionStringBuilder.ConnectionString);


        public BlogAdoDotNetUsingServiceController() { }

        [HttpGet]
        public IActionResult GetAllBlogs()
        {
            string query = "select * from blog";
            var blogs = _adoDotNetService.Query<BlogModel>(query);

            return Ok(blogs);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlogById(int id)
        {
            string query = "select * from blog where id = @id";

            var blog = _adoDotNetService.QueryFirstOrDefault<BlogModel>(query, new AdoDotNetParameter("@id", id));
            if (blog is null)
            {
                return NotFound("no blog found");
            }
            return Ok(blog);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogModel blog)
        {
            string query = @"INSERT INTO [dbo].[blog]
                            ([title],[author],[blogContent])
                            VALUES
                            (@title,@author,@content)";

            int result = _adoDotNetService.Execute(query,
                new AdoDotNetParameter("@title", blog.Title),
                new AdoDotNetParameter("@author", blog.Author),
                new AdoDotNetParameter("@blogContent", blog.Content)
            );

            string responseMessage = ReturnMessages.ManipulatedStatusMessage(result, ManipulationMethods.create);
            return Ok(responseMessage);


        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogRequestDTO blog)
        {
            string query = @"Update [dbo].[blog]
                            SET [title] = @title,
                                [author]=@author,
                                [blogContent]=@content
                            WHERE id = @id";
            int result = _adoDotNetService.Execute(query,
                 new AdoDotNetParameter("@id", id),
                 new AdoDotNetParameter("@title", blog.Title),
                 new AdoDotNetParameter("@author", blog.Author),
                 new AdoDotNetParameter("@blogContent", blog.Content)
             );

            string responseMessage = ReturnMessages.ManipulatedStatusMessage(result, ManipulationMethods.update);
            return Ok(responseMessage);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogRequestDTO requestBlog)
        {
            StringBuilder conditionsBuilder = new();
            List<AdoDotNetParameter> parameters = new();

            if (!string.IsNullOrEmpty(requestBlog.Title))
            {
                conditionsBuilder.Append("[title] = @title, ");
                parameters.Add(new AdoDotNetParameter("@title", requestBlog.Title));
            }

            if (!string.IsNullOrEmpty(requestBlog.Author))
            {
                conditionsBuilder.Append("[author] = @author, ");
                parameters.Add(new AdoDotNetParameter("@author", requestBlog.Author));
            }

            if (!string.IsNullOrEmpty(requestBlog.Content))
            {
                conditionsBuilder.Append("[blogContent] = @blogContent, ");
                parameters.Add(new AdoDotNetParameter("@blogContent", requestBlog.Content));
            }

            string conditions = conditionsBuilder.ToString();

            if (string.IsNullOrEmpty(conditions))
            {
                return NotFound("No data to update.");
            }

            conditions = conditions.Substring(0, conditions.Length - 2);

            string query = $@"UPDATE [dbo].[blog]
                      SET {conditions} 
                      WHERE id = @id";

            parameters.Add(new AdoDotNetParameter("@id", id));

            int result = _adoDotNetService.Execute(query, parameters.ToArray());

            string responseMessage = ReturnMessages.ManipulatedStatusMessage(result, ManipulationMethods.update);
            return Ok(responseMessage);
        }



        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            string query = @"Delete From [dbo].[blog]
                            WHERE id = @id";

            int result = _adoDotNetService.Execute(query,
                new AdoDotNetParameter("@id", id));

           string responseMessage = ReturnMessages.ManipulatedStatusMessage(result, ManipulationMethods.delete);
            return Ok(responseMessage);
        }

    }


}

