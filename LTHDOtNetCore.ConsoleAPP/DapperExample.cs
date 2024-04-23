using Dapper;
using LTHDOtNetCore.ConsoleAPP.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTHDOtNetCore.ConsoleAPP
{
    internal class DapperExample
    {

        public void Run()
        {
            GetAll();
            GetById(2);
            Create("Dapper Title","Dapper Author","Dapper Content");
            Update(1, "Updated Title", "Updated Author", "Updated Content");
            GetAll();
            Delete(2);
            GetAll();
        }

        private void GetAll()
        {
            using IDbConnection dbConnection = new SqlConnection(ConnectionString.sqlConnectionStringBuilder.ConnectionString);
            List<BlogModel> blogs = dbConnection.Query<BlogModel>("select * from blog").ToList();
            foreach (var blog in blogs)
            {
                PrintBlogData(blog);
            }
        }

        private void GetById(int id)
        {
            using IDbConnection dbConnection = new SqlConnection(ConnectionString.sqlConnectionStringBuilder.ConnectionString);
            var blog = dbConnection.Query<BlogModel>("select * from blog where id = @id",new BlogModel() {Id = id })
                        .FirstOrDefault();
            if(blog is null)
            {
                Console.WriteLine("No Blog Found");
                return;
            }
            PrintBlogData(blog);
        }

        private void Create(string title, string author, string content)
        {
            BlogModel blog = new()
            {
                Title = title,
                Author = author,
                BlogContent = content
            };
            string query = @"INSERT INTO [dbo].[blog]
                            ([title],[author],[blogContent])
                            VALUES
                            (@title,@author,@blogContent)";

            using IDbConnection dbConnection = new SqlConnection(ConnectionString.sqlConnectionStringBuilder.ConnectionString);
            int result = dbConnection.Execute(query, blog);
            string message = result > 0 ? "Successfully Created" : "Create Fail";
            Console.WriteLine("---------");
            Console.WriteLine(message);
            Console.WriteLine("---------");
        }

        private void Update(int id, string title, string author, string content)
        {
            BlogModel blog = new()
            {
                Id = id,
                Title = title,
                Author = author,
                BlogContent = content
            };
            string query = @"Update [dbo].[blog]
                            SET [title] = @title,
                                [author]=@author,
                                [blogContent]=@blogContent
                            WHERE id = @id";
            using IDbConnection dbConnection = new SqlConnection(ConnectionString.sqlConnectionStringBuilder.ConnectionString);
            int result = dbConnection.Execute(query, blog);
            string message = result > 0 ? "Successfully Updated" : "Update Fail";
            Console.WriteLine("---------");
            Console.WriteLine(message);
            Console.WriteLine("---------");

        }

        private void Delete(int id)
        {
            using IDbConnection dbConnection = new SqlConnection(ConnectionString.sqlConnectionStringBuilder.ConnectionString);
            int result = dbConnection.Execute("Delete From [dbo].[blog] WHERE id = @id", new { id });
            string message = result > 0 ? "Successfully Deleted" : "Delete Fail";
            Console.WriteLine("---------");
            Console.WriteLine(message);
            Console.WriteLine("---------");
        }


        private static void PrintBlogData(BlogModel blog)
        {
            Console.WriteLine("---------");
            Console.WriteLine("Id : " + blog.Id);
            Console.WriteLine("Id : " + blog.Title);
            Console.WriteLine("Id : " + blog.Author);
            Console.WriteLine("Id : " + blog.BlogContent);
            Console.WriteLine("---------");
        }

    }
}
