using LTHDOtNetCore.MinimalApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LTHDOtNetCore.MinimalApi.Features.Blog
{
    public static class BlogRoutes
    {
        public static void BlogRoute(this IEndpointRouteBuilder app,string? basePath="")
        {
            string Route(string path) => string.IsNullOrEmpty(basePath) ? path : $"{basePath}{path}";


            app.MapGet(Route(""), async (AppDbContext context) =>
            {
                var blogs = await context.Blogs.AsNoTracking().ToListAsync();
                return Results.Ok(blogs);
            });

            app.MapGet(Route("/{id}"), async (AppDbContext context, int id) =>
            {
                var blog = await context.Blogs.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
                if (blog is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(blog);
            });

            app.MapPost(Route(""), async (AppDbContext context, BlogModel requestBlog) =>
            {
                await context.Blogs.AddAsync(requestBlog);
                await context.SaveChangesAsync();
                return Results.Ok("Successfully Created");
            });

            app.MapPut(Route("/{id}"), async (AppDbContext context, int id, BlogModel requestBlog) =>
            {
                var existingBlog = await context.Blogs.FirstOrDefaultAsync(b => b.Id == id);
                if (existingBlog is null)
                {
                    return Results.NotFound();
                }
                existingBlog.Title = requestBlog.Title;
                existingBlog.Author = requestBlog.Author;
                existingBlog.Content = requestBlog.Content;
                await context.SaveChangesAsync();
                return Results.Ok("Successfully Updated");
            });

            app.MapDelete(Route("/{id}"), async (AppDbContext context, int id) =>
            {
                var existingBlog = await context.Blogs.FirstOrDefaultAsync(b => b.Id == id);
                if (existingBlog is null)
                {
                    return Results.NotFound();
                }
                context.Blogs.Remove(existingBlog);
                await context.SaveChangesAsync();
                return Results.Ok("Successfully Deleted");
            });
        }
    }
}
