using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTHDOtNetCore.ConsoleAppRefitExamples
{
    public class RefitExample
    {
        private readonly IBlogApi _blogApi = RestService.For<IBlogApi>("https://localhost:7250");

        public async Task Run()
        {
            await GetAllBlogsAsync();
            await GetBlogByIdAsync(5);
            await GetBlogByIdAsync(1);
        }

        private async Task GetAllBlogsAsync()
        {
            var blogs = await _blogApi.GetAllBlogs();
            foreach (var blog in blogs)
            {
                PrintBlogData(blog);
            }
        }

        private async Task GetBlogByIdAsync(int id)
        {
            try
            {
                var blog = await _blogApi.GetBlogById(id);
                PrintBlogData(blog);
            }
            catch (ApiException ex)
            {
                Console.WriteLine(ex.StatusCode.ToString());
                Console.WriteLine(ex.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task CreateAsync(string title, string author, string content)
        {
            BlogModel blog = new()
            {
                Title = title,
                Author = author,
                Content = content,
            };

            var message = await _blogApi.CreateBlog(blog);
            Console.WriteLine(message);
        }

        private async Task UpdateAsync(int id, string title, string author, string content)
        {
            BlogModel blog = new BlogModel()
            {
                Title = title,
                Author = author,
                Content = content,
            };

            var message = await _blogApi.UpdateBlog(id, blog);
            Console.WriteLine(message);
        }

        private async Task DeleteAsync(int id)
        {
            var message = await _blogApi.DeleteBlog(id);
            Console.WriteLine(message);
        }

        private static void PrintBlogData(BlogModel blogModel)
        {
            Console.WriteLine("----------");
            Console.WriteLine("Id------" + blogModel.Id);
            Console.WriteLine("Title------" + blogModel.Title);
            Console.WriteLine("Author------" + blogModel.Author);
            Console.WriteLine("Content------" + blogModel.Content);
            Console.WriteLine("----------");
        }
    }
}
