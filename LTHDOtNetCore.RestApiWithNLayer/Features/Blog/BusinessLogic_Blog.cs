namespace LTHDOtNetCore.RestApiWithNLayer.Features.Blog
{
    public class BusinessLogic_Blog
    {
        private readonly DataAccess_Blog _dataAccess_Blog;
        public BusinessLogic_Blog()
        {
            _dataAccess_Blog = new DataAccess_Blog();
        }
        
        public List<BlogModel> GetAllBlogs()
        {
            var blogs = _dataAccess_Blog.GetAllBlogs();
            return blogs;
        }

        public BlogModel? GetBlogById(int id)
        {
            var blog = _dataAccess_Blog.GetBlogById(id);
            return blog;
        }

        public int CreateBlog(BlogModel requestBlog)
        {
            var result = _dataAccess_Blog.CreateBlog(requestBlog);
            return result;
        }

        public int UpdateBlog(int id, BlogModel requestBlog)
        {
            var result = _dataAccess_Blog.UpdateBlog(id, requestBlog);
            return result;
        }

        public int DeleteBlog(int id)
        {
            var result = _dataAccess_Blog.DeleteBlog(id);
            return result;
        }

    }
}
