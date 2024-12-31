using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace Agora.Common.Api;

public class ExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandler> _logger;

    public ExceptionHandler(RequestDelegate next, ILogger<ExceptionHandler> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        _logger.LogError(ex, "An unhandled exception has occurred while executing the request.");
        var code = HttpStatusCode.InternalServerError;
        var result = JsonSerializer.Serialize(new { error = ex.Message });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result);
    }
}
