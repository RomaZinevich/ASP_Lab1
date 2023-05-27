namespace LoggerMiddleware
{
    public class LoggerMiddlewaree
    {
        private readonly RequestDelegate _next;
        public LoggerMiddlewaree(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            File.AppendAllText("access.txt", $"{DateTime.Now.ToString()} {context.Request.Path}\n");
            await _next.Invoke(context);
        }
    }
    public class SecretMiddleware
    {
        private readonly RequestDelegate _next;
        public SecretMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            string path = context.Request.Path.ToString().ToLower();
            if (path == "/secret")
                await context.Response.WriteAsync("Secret text");
        }
    }
}
