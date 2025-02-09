using CallerApi.Dtos.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CallerApi.Business.Interfaces;
using CallerApi.Dtos;
using AutoMapper;
using CallerApi.Domain;

namespace CallerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MainController : ControllerBase
{
    private ILogger<MainController> _logger;
    private IRequestService _requestService;
    private IMapper _mapper;

    public MainController(
        ILogger<MainController> logger, IRequestService requestService, IMapper mapper)
    {
        _logger = logger;
        _requestService = requestService;
        _mapper = mapper;
    }

    [HttpGet("GetExampleTrail")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorMessageDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorMessageDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetExampleTrail([FromQuery] bool error)
    {
        try
        {
            _logger.LogInformation($"GetExample");

            var result = await _requestService.GetExample(error);

            if(result.Success)
            {
                return NoContent();
            }
            else
            {
                var message = $"An error has ocurred.";
                _logger.LogError(message);
                return BadRequest(new ErrorMessageDto() { Message = message });
            }
        }
        catch (Exception exception)
        {
            var message = $"An exception has ocurred. Message:{exception.Message}";
            _logger.LogError(exception, message);
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorMessageDto() { Message = message });
        }
    }

    [HttpPost("PostExampleTrail")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorMessageDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorMessageDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PostExampleTrail([FromBody] PostExampleRequestDto request)
    {
        try
        {

            _logger.LogInformation($"PostExample");

            var result = _mapper.Map<PostExampleResponseDto>(await _requestService.PostExample(_mapper.Map<PostExampleRequest>(request)));

            if(result?.Success == true)
            {
                return Ok(result);
            }
            else
            {
                var message = $"An error has ocurred.";
                _logger.LogError(message);
                return BadRequest(new ErrorMessageDto() { Message = message });
            }
        }
        catch (Exception exception)
        {
            var message = $"An exception has ocurred. Message:{exception.Message}";
            _logger.LogError(exception, message);
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorMessageDto() { Message = message });
        }
    }

}
