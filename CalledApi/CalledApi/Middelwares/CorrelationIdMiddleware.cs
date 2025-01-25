using System;
using Serilog;
using Serilog.Context;

namespace CalledApi.Middelwares;

public class CorrelationIdMiddleware
{
    private readonly RequestDelegate _next;

    public CorrelationIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var correlationId = Guid.NewGuid().ToString();

        LogContext.PushProperty("CorrelationId", correlationId);

        context.Items["CorrelationId"] = correlationId;

        await _next(context);
    }
}
