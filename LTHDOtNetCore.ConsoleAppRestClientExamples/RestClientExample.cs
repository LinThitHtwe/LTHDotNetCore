using LTHDOtNetCore.ConsoleAppRestClientExamples;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LTHDOtNetCore.ConsoleAppRestClientExamples
{
    internal class RestClientExample
    {
        private readonly RestClient _restClient = new(new Uri("https://localhost:7250"));
        private readonly string _blogEndpoint = "api/blog";

        public async Task RunAsync()
        {
            await GetAllBlogsAsync();
            await GetBlogByIdAsync(1);
            // await CreateAsync("HttpClientTitle", "HttpClientAuthor", "HttpClientContent");
        }

        private async Task GetAllBlogsAsync()
        {
            RestRequest restRequest = new(_blogEndpoint, Method.Get);
            var response = await _restClient.ExecuteAsync(restRequest);
            if (response.IsSuccessStatusCode)
            {
                string jsonString = response.Content!;
                List<BlogDTO> blogs = JsonConvert.DeserializeObject<List<BlogDTO>>(jsonString)!;
                foreach (var blog in blogs)
                {
                    PrintBlogData(blog);
                }
            }
        }

        private async Task GetBlogByIdAsync(int id)
        {
            RestRequest restRequest = new($"{_blogEndpoint}/{id}", Method.Get);
            var response = await _restClient.ExecuteAsync(restRequest);
            if (!response.IsSuccessStatusCode)
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }
            string jsonString = response.Content!;
            var blog = JsonConvert.DeserializeObject<BlogDTO>(jsonString);
            PrintBlogData(blog!);

        }

        private async Task CreateAsync(string title, string author, string content)
        {
            BlogDTO requestBlog = new()
            {
                Title = title,
                Author = author,
                Content = content
            };

            var restRequest = new RestRequest(_blogEndpoint, Method.Put);
            restRequest.AddJsonBody(requestBlog);
            var response = await _restClient.ExecuteAsync(restRequest);
            string message = response.Content!;
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

            var restRequest = new RestRequest(_blogEndpoint, Method.Put);
            restRequest.AddJsonBody(requestBlog);
            var response = await _restClient.ExecuteAsync(restRequest);
            string message = response.Content!;
            Console.WriteLine(message);
        }


        private async Task DeleteAsync(int id)
        {
            RestRequest restRequest = new($"{_blogEndpoint}/{id}", Method.Delete);
            var response = await _restClient.ExecuteAsync(restRequest);

            string message = response.Content!;
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
