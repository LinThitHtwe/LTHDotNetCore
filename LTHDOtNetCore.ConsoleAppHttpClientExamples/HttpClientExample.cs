using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace LTHDOtNetCore.ConsoleAppHttpClientExamples
{
    internal class HttpClientExample
    {
        private readonly HttpClient _httpClient = new() { BaseAddress = new Uri("https://localhost:7250") };
        private readonly string _blogEndpoint = "api/blog";

        public async Task RunAsync()
        {
            await GetAllBlogsAsync();
            await GetBlogByIdAsync(1);
           // await CreateAsync("HttpClientTitle", "HttpClientAuthor", "HttpClientContent");
        }

        private async Task GetAllBlogsAsync()
        {
            var response = await _httpClient.GetAsync(_blogEndpoint);
            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                List<BlogDTO> blogs = JsonConvert.DeserializeObject<List<BlogDTO>>(jsonString)!;
                foreach (var blog in blogs)
                {
                    PrintBlogData(blog);
                }
            }
        }

        private async Task GetBlogByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_blogEndpoint}/{id}");
            if (!response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
            string jsonString = await response.Content.ReadAsStringAsync();
            var blog = JsonConvert.DeserializeObject<BlogDTO>(jsonString);
            PrintBlogData(blog);

        }

        private async Task CreateAsync(string title, string author, string content)
        {
            BlogDTO requestBlog = new()
            {
                Title = title,
                Author = author,
                Content = content
            };

            string blogJsonString = JsonConvert.SerializeObject(requestBlog);

            HttpContent httpContent = new StringContent(blogJsonString, Encoding.UTF8, Application.Json);
            var response = await _httpClient.PostAsync(_blogEndpoint, httpContent);
            string message = await response.Content.ReadAsStringAsync();
            Console.WriteLine(message);
        }

        private async Task UpdateAsync(int id, string title, string author, string content)
        {
            BlogDTO requestBlog = new()
            {
                Title = title,
                Author = author,
                Content = content
            };

            string blogJsonString = JsonConvert.SerializeObject(requestBlog);

            HttpContent httpContent = new StringContent(blogJsonString, Encoding.UTF8, Application.Json);
            var response = await _httpClient.PutAsync($"{_blogEndpoint}/{id}", httpContent);
            string message = await response.Content.ReadAsStringAsync();
            Console.WriteLine(message);
        }


        private async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_blogEndpoint}/{id}");

            string message = await response.Content.ReadAsStringAsync();
            Console.WriteLine(message);
        }


        private static void PrintBlogData(BlogDTO blog)
        {
            Console.WriteLine("<------->");
            Console.WriteLine("Id--->" + blog.Id);
            Console.WriteLine("Title--->" + blog.Title);
            Console.WriteLine("Author--->" + blog.Author);
            Console.WriteLine("Content--->" + blog.Content);
            Console.WriteLine("<------->");
        }
    }
}
