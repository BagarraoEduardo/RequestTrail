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

    [HttpGet("SuccessExample")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorMessageDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorMessageDTO), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SuccessExample()
    {
        try
        {
            _logger.LogInformation("Success");

            return NoContent();
        }
        catch (Exception exception)
        {
            var message = $"An exception has ocurred. Message:{exception.Message}";
            _logger.LogError(exception, message);
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorMessageDTO() { Message = message });
        }
    }

    [HttpGet("BadRequestExample")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorMessageDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorMessageDTO), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> BadRequestExample()
    {
        try
        {
            var message = "BadRequest";
            _logger.LogError(message);
            return BadRequest(new ErrorMessageDTO() { Message = message });
        }
        catch (Exception exception)
        {
            var message = $"An exception has ocurred. Message:{exception.Message}";
            _logger.LogError(exception, message);
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorMessageDTO() { Message = message });
        }
    }

    [HttpGet("InternalServerErrorExample")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorMessageDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorMessageDTO), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> InternalServerErrorExample()
    {
        try
        {
            var message = "Internal Server Error";
            _logger.LogError(message);
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorMessageDTO() { Message = message });
        }
        catch (Exception exception)
        {
            var message = $"An exception has ocurred. Message:{exception.Message}";
            _logger.LogError(exception, message);
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorMessageDTO() { Message = message });
        }
    }
}