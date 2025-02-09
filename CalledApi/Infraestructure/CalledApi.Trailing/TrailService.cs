// using System.Data;
// using System.Runtime.CompilerServices;
// using Microsoft.Extensions.Logging;
// using MySqlConnector;

// namespace CalledApi.Trailing;

// public interface ITrailService
// {
//     Task<(bool Success, string ErrorMessage, long Id)> InsertTrailAsync(Trail trail);
//     Task<(bool Success, string ErrorMessage)> UpdateTrailAsync(Trail trail);
// }


// public class TrailService : ITrailService
// {
//     private readonly ITrailDataAccess _dataAccess;
//     private readonly ILogger<TrailService> _logger;

//     public TrailService(ITrailDataAccess dataAccess, ILogger<TrailService> logger)
//     {
//         _dataAccess = dataAccess;
//         _logger = logger;
//     }

//     public async Task<(bool Success, string ErrorMessage, long Id)> InsertTrailAsync(Trail trail)
//     {
//         (bool Success, string ErrorMessage, long Id) response = (false, string.Empty, -1);

//         try
//         {
//             response = await _dataAccess.InsertTrailAsync(trail);
//         }
//         catch(Exception exception)
//         {
//             var message = $"An exception has ocurred on the business while logging the HTTP trail. Message: {exception.Message}";
//             _logger.LogError(exception, message);
//             response.ErrorMessage = message;
//         }

//         return response;
//     }

//     public async Task<(bool Success, string ErrorMessage)> UpdateTrailAsync(Trail trail)
//     {
//         (bool Success, string ErrorMessage) response = (false, string.Empty);

//         try
//         {
//             response = await _dataAccess.UpdateTrailAsync(trail);
//         }
//         catch(Exception exception)
//         {
//             var message = $"An exception has ocurred on the business while updating the HTTP trail. Message: {exception.Message}";
//             _logger.LogError(exception, message);
//             response.ErrorMessage = message;
//         }

//         return response;
//     }
// }
