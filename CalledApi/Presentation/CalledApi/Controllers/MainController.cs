using CalledApi.Dtos.Base;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CalledApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MainController : ControllerBase
{
    private ILogger<MainController> _logger;

    public MainController(
        ILogger<MainController> logger)
    {
        _logger = logger;
    }

    [HttpGet("GetExample")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorMessageDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorMessageDTO), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetExample([FromQuery] bool error)
    {
        try
        {
            if (error)
            {
                _logger.LogInformation($"Bad Request.");
                return BadRequest(new ErrorMessageDTO()
                {
                    Message = "Bad Request."
                });
            }
            else
            {
                _logger.LogInformation($"Success.");
                return NoContent();
            }
        }
        catch (Exception exception)
        {
            var message = $"An exception has ocurred. Message:{exception.Message}";
            _logger.LogError(exception, message);
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorMessageDTO()
            {
                Message = message
            });
        }
    }

    [HttpPost("PostExample")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(BaseResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorMessageDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorMessageDTO), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PostExample([FromBody] BaseRequestDTO request)
    {
        try
        {
            if(request.Error)
            {
                _logger.LogInformation($"Bad Request.");
                return BadRequest(new ErrorMessageDTO()
                {
                    Message = "Bad Request."
                });
            }
            else
            {
                return Ok(new BaseResponseDTO()
                {
                    Message = "Success."
                });
            }
        }
        catch (Exception exception)
        {
            var message = $"An exception has ocurred. Message:{exception.Message}";
            _logger.LogError(exception, message);
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorMessageDTO() { Message = message });
        }
    }
}