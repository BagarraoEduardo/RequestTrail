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

    [HttpGet("SuccessfulTrailExample")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorMessageDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorMessageDTO), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> LogInfoExample()
    {
        try
        {
            _logger.LogInformation("This is a log test.");

            return NoContent();
        }
        catch (Exception exception)
        {
            var message = $"An exception has ocurred. Message:{exception.Message}";
            _logger.LogError(exception, message);
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorMessageDTO() { Message = message });
        }
    }
}