using LTHDOtNetCore.MinimalApi;
using LTHDOtNetCore.MinimalApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
},
ServiceLifetime.Transient,
ServiceLifetime.Transient);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/helloWorld", () => { Console.WriteLine("Hello"); });

app.MapGet("/blog", async (AppDbContext context) =>
{
    var blogs = await context.Blogs.AsNoTracking().ToListAsync();
    return Results.Ok(blogs);
});

app.MapGet("/blog/{id}", async (AppDbContext context, int id) =>
{
    var blog = await context.Blogs.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
    if (blog is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(blog);
});

app.MapPost("/blog", async (AppDbContext context, BlogModel requestBlog) =>
{
    await context.Blogs.AddAsync(requestBlog);
    await context.SaveChangesAsync();
    return Results.Ok("Successfully Created");
});

app.MapPut("/blog/{id}",async (AppDbContext context,int id,BlogModel requestBlog) =>
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

app.MapDelete("/blog/{id}", async (AppDbContext context, int id) =>
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

app.Run();
