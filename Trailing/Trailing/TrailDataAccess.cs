using System;
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MySqlConnector;

namespace Trailing;

public interface ITrailDataAccess
{
    Task<(bool Success, string ErrorMessage, long Id)> InsertTrailAsync(Trail trail);
    Task<(bool Success, string ErrorMessage)> UpdateTrailAsync(Trail trail);
}

public class TrailDataAccess : ITrailDataAccess
{

    private readonly IConfiguration _configuration;
    private readonly ILogger<TrailDataAccess> _logger;
    private readonly TrailOptions _options;

    public TrailDataAccess(ILogger<TrailDataAccess> logger, IConfiguration configuration, IOptions<TrailOptions> options)
    {
        _logger = logger;
        _configuration = configuration;
        _options = options.Value;
    }

    public async Task<(bool Success, string ErrorMessage, long Id)> InsertTrailAsync(Trail trail)
    {
        (bool Success, string ErrorMessage, long Id) response = (false, string.Empty, -1);

        try
        {
            string insertTrailCommand =
            @"
                INSERT INTO
                    " + _options.DatabaseName + @".Trail
                    (
                        RequestTimestamp,
                        RequestUri,
                        RequestHeaders,
                        RequestBody,
                        CorrelationId
                    )
                VALUES
                    (
                        @RequestTimestamp,
                        @RequestUri,
                        @RequestHeaders,
                        @RequestBody,
                        @CorrelationId
                    );

                    SELECT LAST_INSERT_ID();
            ";

            using var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await connection.OpenAsync();

            using var command = new MySqlCommand(insertTrailCommand, connection);
            command.Parameters.AddWithValue("@RequestTimestamp", trail.RequestTimestamp);
            command.Parameters.AddWithValue("@RequestUri", trail.RequestUri);
            command.Parameters.AddWithValue("@RequestHeaders", trail.RequestHeaders?.Replace("\n", "")?.Replace("\r", ""));
            command.Parameters.AddWithValue("@RequestBody", trail.RequestBody?.Replace("\n", "")?.Replace("\r", ""));
            command.Parameters.AddWithValue("@CorrelationId", trail.CorrelationId.ToString());

            var generatedId = await command.ExecuteScalarAsync();
            var isValidId = Int64.TryParse(generatedId?.ToString(), out long parsedId);

            if(isValidId)
            {
                response.Id = parsedId;
                response.Success = true;
            }
            else
            {
                var message = $"An error ocurred while inserting Trail record. Id:{trail.Id}";
                _logger.LogError(message);
                response.ErrorMessage = message;
            }
        }
        catch (Exception exception)
        {
            var message = $"An exception has ocurred on the DataAccess while inserting the Trail record. Id:{trail.Id} Message:{exception.Message}";
            _logger.LogError(exception, message);
            response.ErrorMessage = message;
        }

        return response;
    }

    public async Task<(bool Success, string ErrorMessage)> UpdateTrailAsync(Trail trail)
    {
        (bool Success, string ErrorMessage) response = (false, string.Empty);
        try
        {
            string updateTrailCommand =
            @"
                UPDATE
                    " + _options.DatabaseName + @".Trail
                SET
                    ResponseTimestamp=@ResponseTimestamp,
                    ResponseBody=@ResponseBody,
                    StatusCode=@StatusCode
                WHERE Id=@Id
            ";

            using var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await connection.OpenAsync();

            using var command = new MySqlCommand(updateTrailCommand, connection);
            command.Parameters.AddWithValue("@ResponseTimestamp", trail.ResponseTimestamp);
            command.Parameters.AddWithValue("@ResponseBody", trail.ResponseBody?.Replace("\n", "").Replace("\r", ""));
            command.Parameters.AddWithValue("@StatusCode", trail.StatusCode?.Trim());
            command.Parameters.AddWithValue("@Id", trail.Id);

            var result = await command.ExecuteNonQueryAsync();

            response.Success = result > 0;

            if(!response.Success)
            {
                var message = $"An error ocurred while updating Trail record. Id:{trail.Id}";
                _logger.LogError(message);
                response.ErrorMessage = message;
            }
        }
        catch (Exception exception)
        {
            var message = $"An exception has ocurred on the DataAccess while updating the Trail record. Id:{trail.Id} Message:{exception.Message}";
            _logger.LogError(exception, message);
            response.ErrorMessage = message;
        }

        return response;
    }
}
