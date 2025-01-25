using System;
using Serilog;
using Serilog.Context;

namespace CalledApi.Middelwares;

public class CorrelationIdMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CorrelationIdMiddleware> _logger;

    public CorrelationIdMiddleware(RequestDelegate next, ILogger<CorrelationIdMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            const string correlationIdHeaderKey = "X-Correlation-ID";
            string correlationId;

            if (context.Request.Headers.TryGetValue(correlationIdHeaderKey, out var correlationIdHeader))
            {
                correlationId = correlationIdHeader.ToString();
            }
            else
            {
                correlationId = Guid.NewGuid().ToString();
                context.Request.Headers[correlationIdHeaderKey] = correlationId;
            }

            LogContext.PushProperty("CorrelationId", correlationId);

            await _next(context);
        }
        catch(Exception exception)
        {
            _logger.LogError(exception, $"An exception has ocurred. Message: {exception.Message}");
        }
    }
}
