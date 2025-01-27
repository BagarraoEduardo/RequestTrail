using CalledApi.Trailing;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace Trailing;

public class TrailMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<TrailMiddleware> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public TrailMiddleware(
        RequestDelegate next,
        ILogger<TrailMiddleware> logger,
        IServiceScopeFactory serviceScopeFactory)
    {
        _next = next;
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            const string correlationIdHeaderKey = "CorrelationId";
            Guid correlationId = Guid.Empty;

            if (context.Request.Headers.TryGetValue(correlationIdHeaderKey, out var correlationIdHeader))
            {
                correlationId = new Guid(correlationIdHeader.ToString());
            }
            else
            {
                correlationId = Guid.NewGuid();
                context.Request.Headers[correlationIdHeaderKey] = correlationId.ToString();
            }

            using (LogContext.PushProperty("CorrelationId", correlationId.ToString()))
            {
                var trail = new Trail()
                {
                    RequestTimestamp = DateTime.Now,
                    RequestUri = context.Request.Path,
                    RequestHeaders = context.Request.Headers.ToString() ?? string.Empty,
                    CorrelationId = correlationId
                };

                context.Request.EnableBuffering();

                using (var reader = new StreamReader(context.Request.Body))
                {
                    context.Request.Body.Position = 0;
                    trail.RequestBody = await reader.ReadToEndAsync();
                    context.Request.Body.Position = 0;
                }

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var trailingService = scope.ServiceProvider.GetRequiredService<ITrailService>();

                    var insertTrailResponse = await trailingService.InsertTrailAsync(trail);

                    if (!insertTrailResponse.Success)
                    {
                        _logger.LogError("An error ocurred in the middleware while inserting the trail record.");
                    }

                    var originalBodyStream = context.Response.Body;

                    using (var responseBody = new MemoryStream())
                    {
                        context.Response.Body = responseBody;

                        await _next(context);

                        context.Response.Body.Seek(0, SeekOrigin.Begin);
                        var responseText = await new StreamReader(context.Response.Body).ReadToEndAsync();
                        context.Response.Body.Seek(0, SeekOrigin.Begin);

                        trail.Id = insertTrailResponse.Id;
                        trail.ResponseBody = responseText;
                        trail.ResponseTimestamp = DateTime.Now;
                        trail.StatusCode = context.Response.StatusCode.ToString();

                        var updateTrailResponse = await trailingService.UpdateTrailAsync(trail);

                        if (!updateTrailResponse.Success)
                        {
                            _logger.LogError("An error ocurred in the middleware while updating the trail record.");
                        }

                        await responseBody.CopyToAsync(originalBodyStream);
                    }
                }
            }
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"An exception has ocurred. Message: {exception.Message}");
        }
    }
}
