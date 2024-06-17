using LTHDOtNetCore.NLayer.DataAccess;

Console.WriteLine("Hello, World!");

BusinessLogic_Blog bL_Blog = new();
var blogs = bL_Blog.GetAllBlogs();
foreach(var blog in blogs)
{
    Console.WriteLine(blog.Id);
}