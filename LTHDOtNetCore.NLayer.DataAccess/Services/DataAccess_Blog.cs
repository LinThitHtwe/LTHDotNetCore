using Microsoft.EntityFrameworkCore;

namespace LTHDOtNetCore.NLayer.DataAccess
{
    public class DataAccess_Blog
    {
        private readonly AppDbContext _appDbContext;

        public DataAccess_Blog()
        {
            _appDbContext = new AppDbContext();
        }

        public List<BlogModel> GetAllBlogs()
        {
            var blogs = _appDbContext.Blogs.AsNoTracking().ToList();
            return blogs;
        }

        public BlogModel? GetBlogById(int id)
        {
            var blogs = _appDbContext.Blogs.FirstOrDefault(blog => blog.Id == id);
            return blogs;
        }

        public int CreateBlog(BlogModel requestBlog)
        {
            _appDbContext.Blogs.Add(requestBlog);
            var result = _appDbContext.SaveChanges();
            return result;
        }

        public int UpdateBlog(int id, BlogModel requestBlog)
        {
            var blog = _appDbContext.Blogs.FirstOrDefault(b => b.Id == id);
            if (blog is null) return 0;

            blog.Title = requestBlog.Title;
            blog.Author = requestBlog.Author;
            blog.Content = requestBlog.Content;

            var result = _appDbContext.SaveChanges();
            return result;
        }

        public int DeleteBlog(int id)
        {
            var blog = _appDbContext.Blogs.FirstOrDefault(b => b.Id == id);
            if (blog is null) return 0;

            _appDbContext.Blogs.Remove(blog);
            var result = _appDbContext.SaveChanges();
            return result;
        }
    }
}
