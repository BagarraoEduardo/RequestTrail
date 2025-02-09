using AutoMapper;
using CallerApi.Business.Interfaces;
using CallerApi.Domain;
using CallerApi.Domain.Base;
using CallerApi.Integration.Generated.CalledApi;
using CallerApi.Integration.Interfaces;
using Microsoft.Extensions.Logging;

namespace CallerApi.Business;

public class RequestService : IRequestService
{
    private readonly IMainClient _mainClient;
    private readonly ILogger<RequestService> _logger;

    private readonly IMapper _mapper;

    public RequestService(
        ILogger<RequestService> logger,
        IMainClient mainClient,
        IMapper mapper)
    {
        _logger = logger;
        _mainClient = mainClient;
        _mapper = mapper;
    }

    public async Task<BaseResponse> GetExample(bool error)
    {

        var response = new BaseResponse();

        try
        {
            response = _mapper.Map<BaseResponse>(await _mainClient.GetExample(error));
        }
        catch(Exception exception)
        {
            var message = $"An exception has ocurred. Message: {exception.Message}";
            _logger.LogError(exception, message);
            response.ErrorMessage = message;
        }

        return response;
    }

    public async Task<PostExampleResponse> PostExample(PostExampleRequest request)
    {
        var response = new PostExampleResponse();

        try
        {
            response = _mapper.Map<PostExampleResponse>(await _mainClient.PostExample(_mapper.Map<BaseRequestDTO>(request)));
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
