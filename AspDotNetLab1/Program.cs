using LoggerMiddleware;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseFileServer();

app.UseFileServer(new FileServerOptions
{
    EnableDirectoryBrowsing = true,
    FileProvider = new PhysicalFileProvider(
Path.Combine(Directory.GetCurrentDirectory(), @"static"))
});

app.UseStatusCodePages(async context =>
{
    var response = context.HttpContext.Response;
    if (response.StatusCode == 404)
    {
        context.HttpContext.Response.ContentType = "text/html";
        await response.SendFileAsync("static/error/404.html");
    }
});

app.MapGet("/home/index", async (context) =>
{
    context.Response.ContentType = "text/plain; charset=UTF-8";
    await context.Response.WriteAsync("index");
});

app.MapGet("/home/about", async (context) =>
{
    context.Response.ContentType = "text/plain; charset=UTF-8";
    await context.Response.WriteAsync("about");
});

app.UseMiddleware<LoggerMiddlewaree>();
app.UseMiddleware<SecretMiddleware>();

app.Run();
