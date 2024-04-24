using LTHDOtNetCore.ConsoleAPP.Connections;
using LTHDOtNetCore.ConsoleAPP.Helper;
using LTHDOtNetCore.ConsoleAPP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LTHDOtNetCore.ConsoleAPP.Enums.Enum;

namespace LTHDOtNetCore.ConsoleAPP.Examples
{
    internal class EFCoreExample
    {
        private readonly AppDbContext _appDbContext;

        public EFCoreExample() 
        {
            _appDbContext = new AppDbContext();
        }

        public void Run()
        {
            //Create("EF Title", "EF Author", "EF Content");
            Update(6,"EF Update Title", "EF Update Author", "EF Update Content");
            GetAll();
            //GetById(1);
        }
        private void GetAll()
        {
            var blogs = _appDbContext.Blogs.OrderBy(blog=>blog.Id).ToList();
            foreach(var blog in blogs)
            {
                PrintData.PrintBlogData(blog);
            }
        }

        private void GetById(int id)
        {
            var blog = _appDbContext.Blogs.FirstOrDefault(blog=>blog.Id == id);
            if(blog == null)
            {
                Console.WriteLine("No Blog Found");
                return;
            }
            PrintData.PrintBlogData(blog);
        }

        private void Create(string title,string author,string content)
        {
            BlogModel blog = new()
            { 
            Title = title,
            Author = author,
            Content = content
            };
            _appDbContext.Blogs.Add(blog);
            int result = _appDbContext.SaveChanges();
            PrintData.PrintMutatedStatus(result, ManipulationMethods.create);
        }

        private void Update(int id, string title, string author, string content)
        {
            var blog = _appDbContext.Blogs.FirstOrDefault(blog => blog.Id == id);
            if (blog == null)
            {
                Console.WriteLine("No Blog Found");
                return;
            }
            blog.Title = title;
            blog.Author = author;
            blog.Content = content;
            int result = _appDbContext.SaveChanges();
            PrintData.PrintMutatedStatus(result, ManipulationMethods.update);
        }

        private void Delete(int id)
        {
            var blog = _appDbContext.Blogs.FirstOrDefault(blog => blog.Id == id);
            if (blog == null)
            {
                Console.WriteLine("No Blog Found");
                return;
            }
            _appDbContext.Blogs.Remove(blog);
            int result = _appDbContext.SaveChanges();
            PrintData.PrintMutatedStatus(result, ManipulationMethods.delete);

        }
    }
}
