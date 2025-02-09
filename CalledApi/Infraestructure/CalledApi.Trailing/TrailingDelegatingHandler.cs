// using System;
// using Microsoft.AspNetCore.Http;
// using Microsoft.Extensions.Logging;

// namespace CalledApi.Trailing;


// public class TrailDelegatingHandler : DelegatingHandler
// {
//     private readonly IHttpContextAccessor _httpContextAccessor;
//     private readonly ITrailService _trailService;
//     private readonly ILogger<TrailDelegatingHandler> _logger;

//     public TrailDelegatingHandler(IHttpContextAccessor httpContextAccessor, ITrailService trailService, ILogger<TrailDelegatingHandler> logger)
//     {
//         _httpContextAccessor = httpContextAccessor;
//         _trailService = trailService;
//         _logger = logger;
//     }

//     protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
//     {
//         var correlationId = _httpContextAccessor.HttpContext?.Request.Headers["CorrelationId"].FirstOrDefault();
//         if (!string.IsNullOrEmpty(correlationId))
//         {
//             request.Headers.Add("CorrelationId", correlationId);
//         }

//         var trail = new Trail
//         {
//             RequestTimestamp = DateTime.Now,
//             RequestUri = request.RequestUri.ToString(),
//             RequestHeaders = request.Headers.ToString(),
//             CorrelationId = Guid.Parse(correlationId ?? Guid.NewGuid().ToString())
//         };

//         if (request.Content != null)
//         {
//             trail.RequestBody = await request.Content.ReadAsStringAsync();
//         }

//         var insertTrailResponse = await _trailService.InsertTrailAsync(trail);

//         if (!insertTrailResponse.Success)
//         {
//             _logger.LogError("An error ocurred in the middleware while inserting the trail record.");
//         }

//         var response = await base.SendAsync(request, cancellationToken);

//         trail.Id = insertTrailResponse.Id;
//         trail.ResponseTimestamp = DateTime.Now;
//         trail.StatusCode = response.StatusCode.ToString();

//         if (response.Content != null)
//         {
//             trail.ResponseBody = await response.Content.ReadAsStringAsync();
//         }

//         var updateTrailResponse = await _trailService.UpdateTrailAsync(trail);
//         if (!updateTrailResponse.Success)
//         {
//             _logger.LogError("An error occurred while inserting the trail record for the outgoing request.");
//         }

//         return response;
//     }
// }
