namespace SuperHeroAPI.Middlewares;

public static class LogResponseMiddlewareExtensions
{
    public static IApplicationBuilder UseLogResponseMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LogResponsesMiddleware>();
    }
}
public class LogResponsesMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LogResponsesMiddleware> _logger;
    public LogResponsesMiddleware(RequestDelegate next, ILogger<LogResponsesMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        using (var ms = new MemoryStream())
        {
            var originalBodyResponse = context.Response.Body;
            context.Response.Body = ms;

            await _next.Invoke(context);
            
            ms.Seek(0, SeekOrigin.Begin);
            string response = new StreamReader(ms).ReadToEnd();
            ms.Seek(0, SeekOrigin.Begin);

            await ms.CopyToAsync(originalBodyResponse);
            context.Response.Body = originalBodyResponse;
            
            _logger.LogInformation($"{context.Request.Path} {context.Response.StatusCode} {response}");

        }
    }
}   