using LTHDOtNetCore.RestApi.Db;
using LTHDOtNetCore.RestApi.DTOs;
using LTHDOtNetCore.RestApi.Helpers;
using LTHDOtNetCore.RestApi.Models;
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
    public class BlogAdoDotNetController : ControllerBase
    {

        public BlogAdoDotNetController() { }

        [HttpGet]
        public IActionResult GetAllBlogs()
        {
            string query = "select * from blog";

            SqlConnection sqlConnection = new(ConnectionString.sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new(query, sqlConnection);
            SqlDataAdapter sqlDataAdapter = new(sqlCommand);
            DataTable dataTable = new();
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();

            List<BlogModel> blogs = dataTable.AsEnumerable().Select(dataRow => new BlogModel
            {
                Id = Convert.ToInt32(dataRow["id"]),
                Title = Convert.ToString(dataRow["title"]),
                Author = Convert.ToString(dataRow["author"]),
                Content = Convert.ToString(dataRow["blogContent"])
            }).ToList();

            return Ok(blogs);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlogById(int id)
        {
            string query = "select * from blog where id = @id";

            SqlConnection sqlConnection = new(ConnectionString.sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id", id);
            SqlDataAdapter sqlDataAdapter = new(sqlCommand);
            DataTable dataTable = new();
            sqlDataAdapter.Fill(dataTable);

            sqlConnection.Close();

            if (dataTable.Rows.Count == 0)
            {
                return NotFound("No data found.");
            }

            DataRow dataRow = dataTable.Rows[0];
            var blog = new BlogModel
            {
                Id = Convert.ToInt32(dataRow["id"]),
                Title = Convert.ToString(dataRow["title"]),
                Author = Convert.ToString(dataRow["author"]),
                Content = Convert.ToString(dataRow["blogContent"])
            };

            return Ok(blog);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogModel requestBlog)
        {
            string query = @"INSERT INTO [dbo].[blog]
                            ([title],[author],[blogContent])
                            VALUES
                            (@title,@author,@content)";

            SqlConnection connection = new(ConnectionString.sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand sqlCommand = new(query, connection);
            sqlCommand.Parameters.AddWithValue("@title", requestBlog.Title);
            sqlCommand.Parameters.AddWithValue("@author", requestBlog.Author);
            sqlCommand.Parameters.AddWithValue("@content", requestBlog.Content);
            int result = sqlCommand.ExecuteNonQuery();

            connection.Close();

            string responseMessage = ReturnMessages.ManipulatedStatusMessage(result, ManipulationMethods.create);
            return Ok(responseMessage);


        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogRequestDTO requestBlog)
        {
            string query = @"Update [dbo].[blog]
                            SET [title] = @title,
                                [author]=@author,
                                [blogContent]=@content
                            WHERE id = @id";
            SqlConnection sqlConnection = new(ConnectionString.sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id", id);
            sqlCommand.Parameters.AddWithValue("@title", requestBlog.Title);
            sqlCommand.Parameters.AddWithValue("@author", requestBlog.Author);
            sqlCommand.Parameters.AddWithValue("@content", requestBlog.Content);
            int result = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();

            string responseMessage = ReturnMessages.ManipulatedStatusMessage(result, ManipulationMethods.update);
            return Ok(responseMessage);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogRequestDTO requestBlog)
        {
            StringBuilder conditionsBuilder = new();
            List<SqlParameter> parameters = new();

            if (!string.IsNullOrEmpty(requestBlog.Title))
            {
                conditionsBuilder.Append("[title] = @title, ");
                parameters.Add(new SqlParameter("@title", SqlDbType.VarChar) { Value = requestBlog.Title });
            }

            if (!string.IsNullOrEmpty(requestBlog.Author))
            {
                conditionsBuilder.Append("[author] = @author, ");
                parameters.Add(new SqlParameter("@author", SqlDbType.VarChar) { Value = requestBlog.Author });
            }

            if (!string.IsNullOrEmpty(requestBlog.Content))
            {
                conditionsBuilder.Append("[blogContent] = @blogContent, ");
                parameters.Add(new SqlParameter("@blogContent", SqlDbType.VarChar) { Value = requestBlog.Content });
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

            SqlConnection sqlConnection = new(ConnectionString.sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new(query, sqlConnection);

            sqlCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = id });
            sqlCommand.Parameters.AddRange(parameters.ToArray());
            int result = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            string responseMessage = ReturnMessages.ManipulatedStatusMessage(result, ManipulationMethods.update);
            return Ok(responseMessage);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            string query = @"Delete From [dbo].[blog]
                            WHERE id = @id";

            SqlConnection sqlConnection = new(ConnectionString.sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id", id);
            int result = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            string responseMessage = ReturnMessages.ManipulatedStatusMessage(result, ManipulationMethods.delete);
            return Ok(responseMessage);
        }

    }


}

