using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTHDOtNetCore.ConsoleAppRefitExamples
{
    public interface IBlogApi
    {
        [Get("/api/blog")]
        Task<List<BlogModel>> GetAllBlogs();

        [Get("/api/blog/{id}")]
        Task<BlogModel> GetBlogById(int id);

        [Post("/api/blog")]
        Task<string> CreateBlog(BlogModel blog);

        [Put("/api/blog/{id}")]
        Task<string> UpdateBlog(int id, BlogModel blog);

        [Delete("/api/blog/{id}")]
        Task<string> DeleteBlog(int id);

    }
}
