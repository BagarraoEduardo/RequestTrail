using System;
using System.Security;
using CallerApi.Integration.Generated.CalledApi;
using CallerApi.Integration.Interfaces;
using CallerApi.Integration.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CalledApiMainClient = CallerApi.Integration.Generated.CalledApi.MainClient;

namespace CallerApi.Integration.Clients.CalledApi;

public class MainClient : CalledApiMainClient, IMainClient
{
    private readonly ILogger<MainClient> _logger;

    public MainClient(
        IOptions<CalledApiOptions> options,
        HttpClient httpClient,
        ILogger<MainClient> logger) : base(options.Value.Url, httpClient)
    {
        _logger = logger;
    }

    public async Task<(bool Success, string ErrorMessage)> GetExample(bool error)
    {
        (bool Success, string ErrorMessage) response = (false, string.Empty);

        try
        {
            await GetExampleAsync(error);
            response.Success = true;
        }
        catch(ApiException<ErrorMessageDTO> exception)
        {
            var message = $"An exception occurred while calling the API. Message: {exception.Message}";
            _logger.LogError(exception, message);
            response.ErrorMessage = message;
        }
        catch(Exception exception)
        {
            var message = $"An exception has ocurred. Message: {exception.Message}";
            _logger.LogError(exception, message);
            response.ErrorMessage = message;
        }

        return response;
    }

    public async Task<(bool Success, string ErrorMessage, BaseResponseDTO Data)> PostExample(BaseRequestDTO request)
    {
        (bool Success, string ErrorMessage, BaseResponseDTO Data) response = (true, string.Empty, null);

        try
        {
            var result = await PostExampleAsync(request);

            if(result != null)
            {
                response.Success = true;
                response.Data = result;
            }
            else
            {
                var message = $"The retrieved result is null.";
                _logger.LogError(message);
                response.ErrorMessage = message;
            }
        }
        catch(ApiException<ErrorMessageDTO> exception)
        {
            var message = $"An exception occurred while calling the API. Message: {exception.Message}";
            _logger.LogError(exception, message);
            response.ErrorMessage = message;
        }
        catch(Exception exception)
        {
            var message = $"An exception has ocurred. Message: {exception.Message}";
            _logger.LogError(exception, message);
            response.ErrorMessage = message;
        }

        return response;
    }
}
